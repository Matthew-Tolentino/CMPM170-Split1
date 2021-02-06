using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Script must be attached to canvas on which ut displays
public class Puzzle : MonoBehaviour
{
    public GameObject emptyTile;
    public int shuffleTimes = 30;
    public float moveDuration = .2f;
    public float shuffleMoveDuration = .1f;

    enum PuzzleState { Solved, Shuffling, InPlay}
    PuzzleState state;

    private Tile[,] tiles;
    private int shuffleMovesLeft = 0;
    private Vector2Int prevShuffleMove;

    Queue<Tile> inputs;
    bool tileIsMoving;

    void Awake()
    {
        tiles = new Tile[3, 3];
        makePuzzle();
    }

    void Update()
    {
        if (state == PuzzleState.Solved && Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Starting Shuffle");
            StartShuffle();
        }
    }

    public void makePuzzle()
    {
        int posx = 0;
        int posy = 0;
        foreach (Transform child in gameObject.transform)
        {
            if (child.name == "Close") break; // Check to if done with tile children

            Tile tile = child.gameObject.AddComponent<Tile>();
            tile.OnFinishedMoving += OnTileFinishedMoving;
            tiles[posx, posy] = tile;
            tile.startingPos = new Vector2Int(posx, posy);
            tile.position = new Vector2Int(posx, posy++);
            if (posy == 3)
            {
                posy = 0;
                posx++;
            }
        }
        inputs = new Queue<Tile>();
    }

    public void TileClicked()
    {
        MoveTile(EventSystem.current.currentSelectedGameObject.GetComponent<Tile>(), moveDuration);
    }

    private void PlayerMove(Tile tileToMove)
    {
        if (state == PuzzleState.InPlay)
        {
            inputs.Enqueue(tileToMove);
        }
    }

    private void MoveTile(Tile tileToMove, float duration)
    {
        Tile _emptyTile = emptyTile.GetComponent<Tile>();
        if ((tileToMove.position - _emptyTile.position).sqrMagnitude == 1)
        {
            // Update tile array
            tiles[tileToMove.position.x, tileToMove.position.y] = _emptyTile;
            tiles[_emptyTile.position.x, _emptyTile.position.y] = tileToMove;

            // Switch canvas position
            Vector3 temp = emptyTile.transform.position;
            emptyTile.transform.position = tileToMove.transform.position;
            tileToMove.MoveTileToPosition(temp, duration);

            // Switch tile position
            Vector2Int temp2 = _emptyTile.position;
            _emptyTile.position = tileToMove.position;
            tileToMove.position = temp2;

            tileIsMoving = true;
        }
    }

    private void MakeNextPlayerMove()
    {
        while (inputs.Count > 0 && !tileIsMoving)
        {
            MoveTile(inputs.Dequeue(), moveDuration);
        }
    }

    private void OnTileFinishedMoving()
    {
        tileIsMoving = false;
        CheckIfSolved();

        if (state == PuzzleState.InPlay)
        {
            MakeNextPlayerMove();
        }
        else if (state == PuzzleState.Shuffling)
        {
            if (shuffleMovesLeft > 0)
            {
                ShuffleMove();
            } else
            {
                state = PuzzleState.InPlay;
            }
        }
    }

    // Call this method to start shuffling
    public void StartShuffle()
    {
        state = PuzzleState.Shuffling;
        shuffleMovesLeft = shuffleTimes;
        SwitchEmptyTileVisibility(false);
        ShuffleMove();
    }

    void ShuffleMove()
    {
        Vector2Int[] directions = { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1) };
        int randomIndex = Random.Range(0, directions.Length);

        for (int i = 0; i < directions.Length; i++)
        {
            Vector2Int direction = directions[(randomIndex + i) % directions.Length];
            if (direction != prevShuffleMove * -1)
            {
                Vector2Int moveTilePosition = emptyTile.GetComponent<Tile>().position + direction;

                if (moveTilePosition.x >= 0 && moveTilePosition.x < 3 && moveTilePosition.y >= 0 && moveTilePosition.y < 3)
                {
                    MoveTile(tiles[moveTilePosition.x, moveTilePosition.y], shuffleMoveDuration);
                    shuffleMovesLeft--;
                    prevShuffleMove = direction;
                    break;
                }
            }
        }
    }

    void CheckIfSolved()
    {
        foreach (Tile tile in tiles)
        {
            if (!tile.AtStartingPos())
            {
                return;
            }
        }
        state = PuzzleState.Solved;

        // Show final piece of Puzzle
        SwitchEmptyTileVisibility(true);
        Invoke(nameof(ClosePuzzle), 1f);
    }

    void SwitchEmptyTileVisibility(bool visible)
    {
        Image tileImage = emptyTile.GetComponent<Image>();
        Color tempColor = tileImage.color;
        if (!visible) tempColor.a = 0f;
        else tempColor.a = 1f;
        tileImage.color = tempColor;
    }

    void ClosePuzzle()
    {
        gameObject.SetActive(false);

        GameManager.instance.setMouseLock(true);
        // TODO: Add code here to open gate or something after finishing puzzle
    }
}
