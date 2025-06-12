using System.Collections.Generic;
using UnityEngine;

public interface ISingleTargetingStrategy
{
    Transform GetTarget(Transform origin);
}

public interface IMultiTargetingStrategy
{
    List<Transform> GetTargets(Transform origin);
}