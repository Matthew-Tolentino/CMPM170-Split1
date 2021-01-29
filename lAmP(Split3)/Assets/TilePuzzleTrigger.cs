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
            startTilePuzzle();
        }
    }

    private void startTilePuzzle()
    {
        TilePuzzleCanvas.SetActive(true);
        TilePuzzleCanvas.GetComponent<Puzzle>().StartShuffle();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
