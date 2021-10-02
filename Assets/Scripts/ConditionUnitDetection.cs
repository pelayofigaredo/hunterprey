using System.Collections.Generic;
using UnityEngine;

/**
 * Condicion abstarcta que alamacena una lista de transforms
 */
public class ConditionUnitDetection : ICondition
{
    public List<Transform> targetList;
}
