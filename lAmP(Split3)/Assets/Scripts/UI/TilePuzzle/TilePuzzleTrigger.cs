using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePuzzleTrigger : MonoBehaviour
{
    public GameObject TilePuzzleCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartTilePuzzle();
        }
    }

    private void StartTilePuzzle()
    {
        TilePuzzleCanvas.SetActive(true);
        Invoke(nameof(CallShuffle), 1f);

        GameManager.instance.setMouseLock(false);
        GameManager.mouseState = GameManager.MouseState.canvas;
    }

    private void CallShuffle()
    {
        TilePuzzleCanvas.GetComponent<Puzzle>().StartShuffle();
    }
}
