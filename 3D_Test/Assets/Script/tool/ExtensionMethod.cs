using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethod
{
    private const float dotThreshold = 0.5f;//面前范围内60°，角度越大值越小。

    public static bool IsFacingTarget(this Transform transform, Transform target)
    {
        var vectorTarget = target.position - transform.position;
        vectorTarget.Normalize();

        float dot = Vector3.Dot(transform.forward,vectorTarget);
        return dot >= dotThreshold;
    }
}
