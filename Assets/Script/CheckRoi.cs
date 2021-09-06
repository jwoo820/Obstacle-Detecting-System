using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CheckRoi : MonoBehaviour
{
    private static float _roiRadius = 3.0f;

    public static bool Check(Vector3 point)
    {
        Vector3 check = GetCameraPos._userPos - point;
        float size = Vector3.Magnitude(check);
        if (size > _roiRadius) return false;
        else return true;
    }
}
