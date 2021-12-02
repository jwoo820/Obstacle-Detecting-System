using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System;

public class DatabaseBehavior : MonoBehaviour
{
    FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
    public static bool querySuccess = false;

    public void SaveData()
    {
        var lat = GpsManager.current_Lat;
        var lng = GpsManager.current_Long;
        string collection = Truncate(lat).ToString() + "x" + Truncate(lng).ToString();

        float x = GetObstacleUnityPosition().x;
        float z = GetObstacleUnityPosition().z;

        var dis = GetObstacleDistance(x, z);

        ObstacleData data = new ObstacleData
        {
            Latitude = Math.Round(lat, 4),
            Longitude = Math.Round(lng, 4),
            position_x = Math.Round(x, 2),
            position_z = Math.Round(z, 2),
            compass = CompassBehaviour._compass,
            obstacleDis = Math.Round(dis, 2),
            GPS = Math.Round(lat, 4).ToString() + "x" + Math.Round(lng, 4).ToString()
        };

        db.Collection(collection).AddAsync(data).ContinueWithOnMainThread(task =>
        {
            DocumentReference addedDocRef = task.Result;
            Debug.Log("저장완료!");
        });
    }

    public string GetCollection()
    {
        var lat = GpsManager.current_Lat;
        var lng = GpsManager.current_Long;
        string collection = Truncate(lat).ToString() + "x" + Truncate(lng).ToString();
        return collection;
    }

    public string GetDocRef()
    {
        var lat = GpsManager.current_Lat;
        var lng = GpsManager.current_Long;
        string docRef = Math.Round(lat, 4).ToString()
            + "x"
            + Math.Round(lng, 4).ToString();
        return docRef;
    }

    public void QueryData()
    {
        string collection = GetCollection();
        string docRef = GetDocRef();
        int userCom = CompassBehaviour._compass;

        Query GPSQuery = db.Collection(collection)
            .WhereEqualTo(collection, docRef);
        Debug.Log("collection : " + collection);
        Debug.Log("docref : " + docRef);
        GPSQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {

            QuerySnapshot GPSQuerySnapshot = task.Result;

            foreach (DocumentSnapshot documentSnapshot in GPSQuerySnapshot.Documents)
            {
                Debug.Log("Read Success");
                querySuccess = true;
            }
        });

    }

    private Vector3 GetObstacleUnityPosition()
    {
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

    // distance from obstacle
    private float GetObstacleDistance(float x, float z)
    {
        Vector3 pos = new Vector3(x, 0, z);
        Vector3 userPos = new Vector3(GetCameraPos._userPos.x, 0, GetCameraPos._userPos.z);
        var dis = Math.Round(Vector3.Distance(pos,userPos), 2);
        return (float)dis;
    }

    private float Truncate(double n)
    {
        n = Math.Truncate(n * 1000) / 1000;
        return (float)n;
    }
}