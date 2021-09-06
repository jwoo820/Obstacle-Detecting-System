//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using GoogleARCore;
//public class ROI : MonoBehaviour
//{
//    // 1. ROI 평면 부터 계산
//    // Start is called before the first frame update
//    private static float roiRadius = 3.0f;
//    public static bool RoiCheck(Vector3 point)
//    {
//        Vector3 check = Frame.Pose.position - point;
//        float size = Vector3.Magnitude(check);
//        if (size > roiRadius) return false;
//        else return true;
//    }
//}
