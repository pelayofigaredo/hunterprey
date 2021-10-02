using UnityEngine;

/**
 * Condicion que devuelve verdadero cuando la estamina de la unidad sea mayor que el valor dado
 */
public class ConditionUnitStaminaGreaterThan : ICondition
{
	private Unit unit;

	public float value = 50f;

	void Start()
	{
		unit = transform.root.GetComponent<Unit>();
	}

	public override bool Test()
	{
		if (unit == null)
		{
			Debug.LogError("Unit condition not child of unit object");
			return false;
		}
		else if (unit.getStamina() >= value)
		{
			return true;
		}
		return false;
	}
}
