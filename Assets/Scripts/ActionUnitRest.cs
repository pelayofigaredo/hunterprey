using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Accion que hace a la unidad comenzar a descansar
 */
public class ActionUnitRest : ActionUnit
{
    public float restValue = 0.05f;
    public float restVariance = 0.2f;

    private void Start()
    {
        Initialice();
    }

    public override void Act()
    {
        if (!unit.isBusy)
        {
            agent.destination = transform.position;
            unit.IncreaseStamina((restValue + (Random.Range(-restVariance, restVariance))) * Time.deltaTime);
        }

    }
}
