using UnityEngine;

/**
 * Condicion que devuelve verdadero cuando el actual objetivo de la unidad este dentro del rango dado
 */
public class ConditionUnitTargetInRange : ICondition
{
	private Unit unit;

	public float range = 1f;

	void Start()
	{
		unit = transform.root.GetComponent<Unit>();
	}

	public override bool Test()
	{
		if(unit.GetTarget() != null)
        {
			float distance = Vector3.Distance(unit.transform.position, unit.GetTarget().position);
			if (distance <= range)
			{
				return true;
			}
		}
		return false;
	}
}
