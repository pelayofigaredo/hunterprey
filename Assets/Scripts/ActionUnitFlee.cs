
using UnityEngine;


/**
 * Accion que hace a la unidad moverse en direccion cotraria a su objetivo
 */
public class ActionUnitFlee : ActionUnit
{
    public bool refreshTarget = true;

    public override void Act()
    {
        if (!unit.isBusy)
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
            Vector3 direction = unit.transform.position - target.position;
            Vector3 newPos = unit.transform.position + direction;
            agent.SetDestination(newPos);
        }
    }
}
