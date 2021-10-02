using System.Collections;
using UnityEngine;


/**
 * Accion que hace a la unidad entrar en el modo vigilancia
 */

public class ActionUnitWatch : ActionUnit
{
    public float rotationSpeed = 1;

    private void Start()
    {
        Initialice();
    }

    public override void Act()
    {
        unit.transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
        unit.DecreaseStamina(this);
    }
}
