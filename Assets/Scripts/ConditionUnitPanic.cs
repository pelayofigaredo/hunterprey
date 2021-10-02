/**
 * Condicion que devuelve verdadero cuando la unidad esta en pánico
 */
public class ConditionUnitPanic : ICondition
{
    private Unit unit;

    void Start()
    {
        unit = transform.root.GetComponent<Unit>();
    }

    public override bool Test()
    {
        return unit.isInPanic;
    }

    }
