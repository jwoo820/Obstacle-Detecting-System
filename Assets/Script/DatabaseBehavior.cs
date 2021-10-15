using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System;

public class DatabaseBehavior : MonoBehaviour
{
    FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
    // Start is called before the first frame update
    public void SaveData()
    {
        float lat = GpsManager.current_Lat;
        float lng = GpsManager.current_Long;
        string collection = lat.ToString() + "x" + lng.ToString();

        float x = GetObstacleUnityPosition().x;
        float z = GetObstacleUnityPosition().z;
        string gps = GetObstacleGPS(lat, lng, x, z);
        var dis = GetObstacleDistance(x, z);

        //Debug.Log("dis" + dis);
        // 디비 데이터
        ObstacleData data = new ObstacleData
        {
            Latitude = lat,
            Longitude = lng,
            position_x = x,
            position_z = z,
            compass = CompassBehaviour.curr_compass,
            obstacleDis = dis,
            obGPS = gps
        };

        Debug.Log("저장");

        db.Collection(collection).AddAsync(data).ContinueWithOnMainThread(task =>
        {
            DocumentReference addedDocRef = task.Result;
            Debug.Log("success!");
        });
    }

    public void QueryData()
    {
        float lat = GpsManager.current_Lat;
        float lng = GpsManager.current_Long;
        string collection = lat.ToString() + "x" + lng.ToString();

        CollectionReference gpsRef = db.Collection(collection);
        Query query = gpsRef.WhereEqualTo("obGPS", collection);
        query.GetSnapshotAsync().ContinueWithOnMainThread((QuerySnapshotTask) =>
        {
            foreach (DocumentSnapshot doc in QuerySnapshotTask.Result.Documents)
            {
                Debug.Log("FInd ");
            }
        });
    }

    private Vector3 GetObstacleUnityPosition()
    {
        // 장애물의 중심 좌표
        var obstaclePointList = PointCloudVisualization._obstaclePoints;
        var count = obstaclePointList.Count;
        double Obstacle_x = 0;
        double Obstacle_z = 0;
        foreach (var point in obstaclePointList)
        {
            Obstacle_x += point.x;
            Obstacle_z += point.z;
        }

        Vector3 ObstaclePosition = new Vector3(
            (float)Math.Round((Obstacle_x / count), 2),
            0,
            (float)Math.Round((Obstacle_z / count), 2)
            );

        return ObstaclePosition;
    }

    // 장애물 거리 계산
    private float GetObstacleDistance(float x, float z)
    {
        Vector3 pos = new Vector3(x, 0, z);
        Vector3 userPos = new Vector3(GetCameraPos._userPos.x, 0, GetCameraPos._userPos.z);
        var dis = Math.Round(Vector3.Distance(pos, userPos),2);
        return (float)dis;
    }

    private string GetObstacleGPS(float userLat, float userLng, float x, float z)
    {
        int radius = CompassBehaviour._compass;
        float disX = x * Mathf.Cos(radius);
        float disZ = z * Mathf.Sin(radius);

        float obLat = userLat + disX;
        float obLng = userLng + disZ;

        string gps = obLat.ToString() + "x" + obLng.ToString();

        return gps;
    }

}
