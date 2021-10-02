

/**
 * Accion que hace a la unidad entrar en panico
 */
public class ActionUnitPanic : ActionUnit
{

    public override void Act()
    {
        unit.StartPanic();
    }
}
