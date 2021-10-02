using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionUnitModifySenses : ActionUnit
{
    public float listeningDistanceIncrease = 10;
    public float viewAngleIncrease = 10;
    public float viewRadiousIncrease = 10;

    public override void Act()
    {
        unit.ModifySenses(listeningDistanceIncrease, viewAngleIncrease, viewRadiousIncrease);
    }
}
