using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGerbil : Creature
{
    public float freezeRadius = 3.0f;

    public LayerMask freezeables;

    // Start is called before the first frame update
    void Start()
    {
        base.type = Creature.types.time_gerbil;
        base.ability = Creature.abilities.stop_time;
    }

    // Update is called once per frame
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


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, freezeRadius);
    }
}
