using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Child of Creature class
 *      - Ability: Can stop time
 */

public class TimeGerbil : Creature
{
    public float freezeRadius = 3.0f;

    public LayerMask freezeables;


    void Start()
    {
        base.type = Creature.types.time_gerbil;
        base.ability = Creature.abilities.stop_time;
    }


    protected override void Update()
    {
        base.Update();
    }


    // Implement time stop ability
    public override void useAbility()
    {
        // TODO: Play freeze animation for time-gerbil here

        // Detect freezables in range
        Collider2D[] hitFreezables = Physics2D.OverlapCircleAll(transform.position, freezeRadius, freezeables);

        // Freeze the freezables
        foreach(Collider2D obj in hitFreezables)
        {
            if (obj.CompareTag("Enemy"))
            {
                obj.GetComponent<EnemyMovement>().Freeze();
            }
        }
    }

    
    // Show the range of ability in scene view when selected
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, freezeRadius);
    }
}
