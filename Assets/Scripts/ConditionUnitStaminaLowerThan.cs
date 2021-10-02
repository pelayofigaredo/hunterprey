using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionUnitStaminaLowerThan : ICondition
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
		else if (unit.getStamina() <= value)
		{
			return true;
		}
		return false;
	}
}
