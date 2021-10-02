using System.Collections.Generic;
using UnityEngine;


/**
 * Accion que hace a la unidad añadir una lista recogida por una comparacion a sus objetivos
 */
public class ActionUnitAddTargets : ActionUnit
{
    public LayerMask targetLayer;

    public override void Act()
    {
		List<Transform> seeTargets = SeeTargets();
		List<Transform> hearTargets = HearTargets();
		if(seeTargets.Count > 0)
        {
			foreach(Transform t in seeTargets)
            {
				unit.AddTarget(t, true);
            }
        }
		if (hearTargets.Count > 0)
		{
			foreach (Transform t in hearTargets)
			{
				unit.AddTarget(t, false);
			}
		}
	}

	private List<Transform> SeeTargets()
    {
		List<Transform> newTargets = new List<Transform>();
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
						newTargets.Add(target);
					}
				}
			}
		}
		return newTargets;
	}

	private List<Transform> HearTargets()
	{
		List<Transform> newTargets = new List<Transform>();
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, unit.listeningDistance, targetLayer);
		if (hitColliders.Length > 0)
		{
			foreach (Collider c in hitColliders)
			{
				newTargets.Add(c.transform);
			}
		}
		return newTargets;
	}
		

}
