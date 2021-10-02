using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionUnitKnowsTargetPosition : ICondition
{
    private Unit unit;

    void Start()
    {
        unit = transform.root.GetComponent<Unit>();
    }

    public override bool Test()
    {
        return unit.KnowsTargetPos();
    }
}
