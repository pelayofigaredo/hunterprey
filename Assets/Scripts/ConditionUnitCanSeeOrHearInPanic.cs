using System.Collections.Generic;
using UnityEngine;

/**
 * Condicion que devuelve verdadero cuando la unidad puede ver u oir a una unidad en panico
 */

public class ConditionUnitCanSeeOrHearInPanic : ConditionUnitDetection
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
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, unit.viewRadious, targetLayer);
		if (hitColliders.Length > 0)
		{
			foreach (Collider c in hitColliders)
			{
				Transform target = c.transform;
				Vector3 vectorToTarget = target.transform.position - transform.position;
				if (Mathf.Abs(Vector3.Angle(transform.forward, vectorToTarget)) < unit.viewAngle * 0.5f)
				{
					// Last, check target directly visible through ray casting from character
					Ray ray = new Ray(transform.position, vectorToTarget);
					RaycastHit hitInfo;

					if (Physics.Raycast(ray, out hitInfo, unit.viewRadious, targetLayer))
					{
						Unit u = target.GetComponent<Unit>();
						if(u != null && u.isInPanic)
						{
							return true;
						}
					}
				}
			}
			if (targetList.Count > 0)
			{
				return true;
			}

		}
		hitColliders = Physics.OverlapSphere(transform.position, unit.listeningDistance, targetLayer);
		if (hitColliders.Length > 0)
		{
			foreach (Collider c in hitColliders)
			{
				Transform target = c.transform;
					Unit u = target.GetComponent<Unit>();
						if(u != null && u.isInPanic)
						{
							return true;
						}
			}
		}

		return false;
	}
}
