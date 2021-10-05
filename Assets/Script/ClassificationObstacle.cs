using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using System;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Threading.Tasks;
public class ClassificationObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    private int _obstaclePointNum;
    //FirebaseFirestore _firestore;
    FirebaseFirestore db;
    private static WaitForSeconds second;
    private int criteria;
    public GameObject AlertPanel;
    
    IEnumerator Start()
    {
        AlertPanel = GameObject.Find("AlertPanel");
        AlertPanel.SetActive(false);
        criteria = 30;
        db = FirebaseFirestore.DefaultInstance;
        yield return new WaitForSeconds(4f);
        Debug.Log("4초 지났다");
    }

    // Update is called once per frame
    void Update()
    {
        _obstaclePointNum = PointCloudVisualization._obstaclePoints.Count;
        if (_obstaclePointNum > criteria)
        {
            // 휴대폰 진동
            //SaveData();
            Debug.Log("장애물 찾음 !~~~");
            AlertPanel.SetActive(true);
            Handheld.Vibrate();
        }
        else
        {
            AlertPanel.SetActive(false);
        }
    }

    private Vector3 GetObstaclePosition()
    {
        // 장애물의 중심 좌
        var obstaclePointList = PointCloudVisualization._obstaclePoints;
        float Obstacle_x = 0;
        float Obstacle_y = 0;
        float Obstacle_z = 0;
        int count = 0;
        foreach (var point in obstaclePointList)
        {
            Obstacle_x += point.x;
            Obstacle_y += point.y;
            Obstacle_z += point.z;
            count++;
        }
        Vector3 ObstaclePosition = new Vector3(Obstacle_x / count, Obstacle_y / count, Obstacle_z / count);
        return ObstaclePosition;
    }

    private void SaveData()
    {
        // 디비 데이터
        ObstacleData data = new ObstacleData
        {
            Latitude = GpsManager.current_Lat,
            Longitude = GpsManager.current_Long,
            position_x = GetObstaclePosition().x,
            position_y = GetObstaclePosition().y,
            position_z = GetObstaclePosition().z,
            rotation_x = GetCameraPos._userRot.x,
            rotation_y = GetCameraPos._userRot.y,
            rotation_z = GetCameraPos._userRot.z,
            compass = CompassBehaviour.curr_compass
        };
        // 디비 저장
        db.Collection("data").AddAsync(data).ContinueWithOnMainThread(task =>
        {
            DocumentReference addedDocRef = task.Result;
            Debug.Log("success!");
        });
    }
    private void ReadData()
    {
        //    Query allQuery = db.Collection("data").WhereEqualTo("", )
    }
}
