using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Clase que funciona como una estructura de datos para almacenar referencias a los personajes, asi como una variable booleana que representa si 
 * el personaje ha sido visto o no.
 */

public class TargetList 
{

    public Dictionary<Transform, bool> targets = new Dictionary<Transform, bool>();
    public Transform currentTarget;

    public bool AddTarget(Transform target, bool isLocationKnown)
    {
        if (!targets.ContainsKey(target))
        {
            targets.Add(target, isLocationKnown);
            if(currentTarget == null)
            {
                currentTarget = target;
            }
            return true;
        }
        return false;
    }

    public void SetNearestAsCurrent(Transform origin)
    {
        float distance = float.MaxValue;
        if(targets.Count > 0 && origin != null)
        {
            foreach (Transform target in targets.Keys)
            {
                if(target != null)
                {
                    float newDistance = Vector3.Distance(origin.position, target.position);
                    if (newDistance < distance)
                    {
                        distance = newDistance;
                        currentTarget = target;
                    }
                }

            }
        }
        else
        {
            currentTarget = null;
        }
    }

    public void SetRandomAsCurrent()
    {
        if (targets.Count > 0)
        {
            List<Transform> tar = Enumerable.ToList(targets.Keys);
            currentTarget = tar[UnityEngine.Random.Range(0, tar.Count)];
        }
    }

    public void SetNearestVisibleAsCurrent(Transform origin)
    {
        float distance = float.MaxValue;
        if (targets.Count > 0)
        {
            foreach (Transform target in targets.Keys)
            {
                float newDistance = Vector3.Distance(origin.position, target.position);
                if (newDistance < distance && targets[target])
                {
                    distance = newDistance;
                    currentTarget = target;
                }
            }
        }
    }

    public void RemoveTarget()
    {
        targets.Remove(currentTarget);
        currentTarget = null;
    }

    public void TryToRemoveTarget(Transform t)
    {
        if (!isEmpty())
        {
            if (targets.ContainsKey(t))
            {
                targets.Remove(t);
                if (currentTarget.Equals(t))
                {
                    currentTarget = null;
                    if (!isEmpty())
                    {
                        SetRandomAsCurrent();
                    }
                }
            }
        }
    }
    public bool isEmpty()
    {
        if (targets.Count > 0)
        {
            return false;
        }
        return true;
    }

    public bool KnowsPos()
    {
        if(currentTarget != null)
        {
            if (targets[currentTarget])
            {
                return true;
            }
        }
        else if (!isEmpty())
        {
            return SearchForKnowPos();
        }
        return false;
    }  

    private bool SearchForKnowPos()
    {
        if (targets.Count > 0)
        {
            foreach (Transform target in targets.Keys)
            {
                if (targets[target])
                {
                    currentTarget = target;
                    return true;
                }
            }
        }
        return false;
    }
}
