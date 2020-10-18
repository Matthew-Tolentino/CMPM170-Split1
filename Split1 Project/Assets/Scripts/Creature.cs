using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Creature class is the parent class for any creatures added to the game
 *      - Holds creature type and ability
 *      - Takes in an object to follow behind
 *      
 *  Note: enums might be overkill and not really needed to determine creature
 *        type and ability. Not sure if theres a better way to implement.
 */

public class Creature : MonoBehaviour
{
    public enum types {celestial_deer, time_gerbil, space_monkey}

    public enum abilities {fog, stop_time, teleport}

    [Header("Creature Dependencies")]
    public GameObject followObj;

    private Rigidbody rb;

    [Header("Creature Properties")]
    public string Name = null; // Using "name" hides GameObject.name, maybe fix later

    public types type;

    public abilities ability;

    [Header("Follow Properties")]
    public float trailDist = 5.0f;

    public float moveDuration = 3.0f;

    private bool following = false;


    protected virtual void Update()
    {
        // Calculate target position for object to path and start pathing if not currently following
        Vector2 targetPos = followObj.transform.position - (followObj.transform.up * trailDist);
        if (!following) StartCoroutine(Follow(targetPos));
    }


    public virtual void useAbility() 
    {
        // TODO: Implement abilities in child classes
        Debug.LogError("In Creature useAbility(): No child ability implemented");
    }


    // Follow() will move object towards target position over a specified duration (moveDuration)
    // Once within .1 units of target position will snap to target position and followed object's rotation
    private IEnumerator Follow(Vector2 targetPos)
    {
        following = true;
        float time = 0;
        Vector2 startPos = transform.position;
        Vector2 targetVector = targetPos - (Vector2) transform.position;

        while (time < moveDuration && Vector2.Distance(transform.position, targetPos) > 0.1f)
        {
            // Point towards target position
            transform.rotation = Quaternion.LookRotation(Vector3.forward, targetVector);

            // Calculate position to move to and elapsed time during this call
            transform.position = Vector2.Lerp(startPos, targetPos, time / moveDuration);
            time += Time.deltaTime;
            yield return null;
        }
        following = false;
        transform.rotation = followObj.transform.rotation;
        transform.position = targetPos;
    }
}
