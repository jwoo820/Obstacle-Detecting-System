using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;
public class CheckRoi : MonoBehaviour
{
    private static float _planeROI = 5.0f;
    private static float _obstacleROI = 3.0f;

    public static bool PlaneCheck(Vector3 point)
    {
        float size = Vector3.Distance(GetCameraPos._userPos, point);
        if (size > _planeROI) return false;
        else return true;
    }

    public static bool ObstacleCheck(Vector3 point)
    {
        float size = Vector3.Distance(GetCameraPos._userPos, point);
        if (size > _obstacleROI) return false;
        else return true;
    }

    public static bool ROI(Vector3 point)
    {
        bool isRoi = true;
        //Vector3 newPoint = new Vector3(Math.Abs(point.x), Math.Abs(point.y), Math.Abs(point.z));
        Vector3 user = GetCameraPos._userPos;

        // 좌우 기준 roi
        if (point.x - user.x >= 1 && point.x - user.x <= 0)
            isRoi = false;
        // 앞뒤 기준 roi
        if (point.y - user.y <= 1 && point.y - user.y <= -2)
            isRoi = false;
        // 상하 기준 roi
        if (point.z - user.z > -3 && point.z - user.z <= 0)
            isRoi = false;


        return isRoi;
    }
}
