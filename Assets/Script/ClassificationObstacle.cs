using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using System;

public class ClassificationObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PointCloudVisualization._obstaclePointNum > 20)
        {
            Debug.Log("Obstacle Detected!!!");
            Debug.Log("current latitude : " + GpsManager.current_Lat);
            Debug.Log("current longitude : " + GpsManager.current_Long);
            // 휴대폰 진동
            Handheld.Vibrate();
            
        }
    }

    private void OnEnable()
    {

    }
}
