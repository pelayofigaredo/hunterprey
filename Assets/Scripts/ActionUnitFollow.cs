using UnityEngine;


/**
 * Accion que hace a la unidad avanzar hacia su objetivo
 */
public class ActionUnitFollow : ActionUnit
{
    public bool refreshTarget = true;

    public override void Act()
    {
        unit.DecreaseStamina(this);
        Transform target;
        if (refreshTarget)
        {
            target = unit.GetClosestTarget();
        }
        else
        {
            target = unit.GetTarget();
        }
        if(target != null)
        {
            agent.destination = target.position;
        }

    }
}