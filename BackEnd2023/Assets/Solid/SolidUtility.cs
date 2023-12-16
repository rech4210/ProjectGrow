using UnityEngine;
using System.Collections;

public static class SolidUtility
{
    public static Vector3 getAngle2D(Vector3 dic)
    {
        return Vector3.forward * Vector3.SignedAngle(Vector3.right, dic, Vector3.forward);
    }
}

