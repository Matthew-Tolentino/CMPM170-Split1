﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Script must be attached to canvas on which ut displays
public class Puzzle : MonoBehaviour
{
    public GameObject emptyTile;
    public int shuffleTimes = 30;
    public float moveDuration = .2f;
    public float shuffleMoveDuration = .1f;

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
        if (Input.GetKeyDown(KeyCode.O))
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
            Tile tile = child.gameObject.AddComponent<Tile>();
            tile.OnFinishedMoving += OnTileFinishedMoving;
            tiles[posx, posy] = tile;
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
        inputs.Enqueue(tileToMove); 
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

        MakeNextPlayerMove();

        if (shuffleMovesLeft > 0)
        {
            ShuffleMove();
        }
    }

    // Call this method to start shuffling
    public void StartShuffle()
    {
        shuffleMovesLeft = shuffleTimes;
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
}
