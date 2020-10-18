using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Move enemy object horizontal or vertical be able to freeze */

public class EnemyMovement : MonoBehaviour
{
    public enum enemyMovement { horizontal, vertical }

    [Header("Enemy Variables")]
    public enemyMovement movement;

    public float moveSpeed = 5.0f;

    public float maxPositive = 3.0f;

    public float maxNegative = 3.0f;

    private bool positive = true;

    private Vector2 startPos;

    public float freezeDuration = 5.0f;

    private bool frozen = false;

    void Start()
    {
        startPos = transform.position;
    }


    void Update()
    {
        switch (movement)
        {
            case enemyMovement.horizontal:
                if (transform.position.x > startPos.x + maxPositive)
                    positive = false;
                else if (transform.position.x < startPos.x - maxNegative)
                    positive = true;
                break;

            case enemyMovement.vertical:
                if (transform.position.y > startPos.y + maxPositive)
                    positive = false;
                else if (transform.position.y < startPos.y - maxNegative)
                    positive = true;
                break;
        }

        if (positive && !frozen)
        {
            switch (movement)
            {
                case enemyMovement.horizontal:
                    transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                    break;

                case enemyMovement.vertical:
                    transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
                    break;
            }
        }
        else if (!positive && !frozen)
        {
            switch (movement)
            {
                case enemyMovement.horizontal:
                    transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
                    break;

                case enemyMovement.vertical:
                    transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
                    break;
            }
        }
    }

    public void Freeze()
    {
        frozen = true;
        Invoke(nameof(UnFreeze), freezeDuration);
    }

    private void UnFreeze()
    {
        frozen = false;
    }
}
