using UnityEngine;

/**
 * Condicion que devuelve verdadero cuando la unidad tiene un objetivo
 */
public class ConditionUnitHasTarget : ICondition
{
    private Unit unit;

    void Start()
    {
        unit = transform.root.GetComponent<Unit>();
    }

    public override bool Test()
    {
        Transform target = unit.GetTarget();
        if(target == null)
        {
            return false;
        }
        return true;
    }
}
