using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CheckRoi : MonoBehaviour
{
    private static float _planeROI = 5.0f;
    private static float _obstacleROI = 3.0f;

    public static bool PlaneCheck(Vector3 point)
    {
        Vector3 check = GetCameraPos._userPos - point;
        float size = Vector3.Magnitude(check);
        if (size > _planeROI) return false;
        else return true;
    }

    public static bool ObstacleCheck(Vector3 point)
    {
        Vector3 check = GetCameraPos._userPos - point;
        float size = Vector3.Magnitude(check);
        if (size > _obstacleROI) return false;
        else return true;
    }
}
