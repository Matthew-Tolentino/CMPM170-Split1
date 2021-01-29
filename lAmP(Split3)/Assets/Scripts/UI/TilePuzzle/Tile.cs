using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public event System.Action OnFinishedMoving;

    public Vector2Int position;

    public void MoveTileToPosition(Vector3 target, float duration)
    {
        StartCoroutine(TileMove(target, duration));
    }

    // Code based off of Sebastian Lague:
    // https://www.youtube.com/watch?v=X8YnoIq1d1g&list=PLFt_AvWsXl0dWcVz-c5ZoLPeE35dMY_r-
    IEnumerator TileMove(Vector3 target, float duration)
    {
        Vector3 initalPos = transform.position;
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(initalPos, target, percent);
            yield return null;
        }

        if (OnFinishedMoving != null)
        {
            OnFinishedMoving();
        }
    }
}
