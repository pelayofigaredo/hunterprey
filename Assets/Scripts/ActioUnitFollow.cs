using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActioUnitFollow : ActionUnit
{
	public bool shouldStopInstead = false;
	public Transform target;

	public override void Act()
	{
		if (shouldStopInstead)
		{
			agent.isStopped = true;
		}
		else
		{
			agent.isStopped = false;
			agent.destination = target.position;
		}
	}
}
