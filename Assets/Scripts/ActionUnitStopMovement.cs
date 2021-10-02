using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionUnitStopMovement : ActionUnit
{
    public override void Act()
    {
        agent.destination = unit.transform.position;
    }
}
