using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Condicion que devuelve verdadero cuando la unidad puede escuchar al menos un objeto de la mascar indicadda
 * y almacena los que encuentre en una lista
 */
public class ConditionUnitCanHear : ConditionUnitDetection
{
	private Unit unit;
	public LayerMask targetLayer;

	void Start()
	{
		unit = transform.root.GetComponent<Unit>();
	}

	public override bool Test()
	{
		targetList = new List<Transform>();
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, unit.listeningDistance, targetLayer);
		if (hitColliders.Length > 0)
		{
			foreach(Collider c in hitColliders)
            {
				targetList.Add(c.transform);
            }
			return true;
		}
		return false;
	}
}