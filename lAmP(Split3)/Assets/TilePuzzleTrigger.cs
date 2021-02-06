using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePuzzleTrigger : MonoBehaviour
{
    public GameObject TilePuzzleCanvas;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
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
    }

    private void CallShuffle()
    {
        TilePuzzleCanvas.GetComponent<Puzzle>().StartShuffle();
    }
}
