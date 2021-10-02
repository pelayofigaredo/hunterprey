using UnityEngine;


/**
 * Accion que elimina al objetivo de su unidad
 */
public class ActionUnitKillTarget : ActionUnit
{
    public bool refreshTarget = true;
    public override void Act()
    {
        unit.DecreaseStamina(this);
        Transform target = unit.GetTarget();
        Unit victim = target.GetComponent<Unit>();
        if(victim != null)
        {
            unit.RemoveTarget();
            victim.Kill();
            if (refreshTarget)
            {
                unit.RenewTarget();
            }
        }
        else
        {
            Debug.LogError("Trying to kill a non unit");
        }
        
    }
}
