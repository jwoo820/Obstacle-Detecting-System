using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;
using System.Threading;

public class DatabaseBehavior : MonoBehaviour
{
    FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
    public static bool isReadDb = false;

    // Start is called before the first frame update
    public void SaveData()
    {
        var lat = GpsManager.current_Lat;
        var lng = GpsManager.current_Long;
        string collection = Truncate(lat).ToString() + "x" + Truncate(lng).ToString();

        float x = GetObstacleUnityPosition().x;
        float z = GetObstacleUnityPosition().z;

        var dis = GetObstacleDistance(x, z);

        Debug.Log("dis" + dis);
        // 디비 데이터
        ObstacleData data = new ObstacleData
        {
            Latitude = Math.Round(lat, 5),
            Longitude = Math.Round(lng, 5),
            position_x = Math.Round(x, 2),
            position_z = Math.Round(z, 2),
            compass = CompassBehaviour.curr_compass,
            obstacleDis = Math.Round(dis, 2),
            GPS = Math.Round(lat, 5).ToString() + "x" + Math.Round(lng, 5).ToString()
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
        Debug.Log("task 1");
        return collection;
    }

    public string GetDocRef()
    {
        var lat = GpsManager.current_Lat;
        var lng = GpsManager.current_Long;
        string docRef = Math.Round(lat, 5).ToString()
            + "x"
            + Math.Round(lng, 5).ToString();
        Debug.Log("task 2");
        return docRef;
    }

    // 여기서 비동기가 되야함

    public bool QueryData()
    {

        var check = false;
        string collection = GetCollection();
        string docRef = GetDocRef();


        // 이건 무조건 참임
        Query GPSQuery = db.Collection("37.334x127.096").WhereEqualTo("GPS", "1");

        // 비동기 메서드

        GPSQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot GPSQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in GPSQuerySnapshot.Documents)
            {
                    //Debug.Log(String.Format("Document data for {0} document:", documentSnapshot.Id));
                    Debug.Log("task 3");
                check = true;
            };
        });
        //Query GPSQuery = db.Collection(collection).WhereEqualTo("GPS", docRef);
        //Debug.Log("collection Name : " + collection);
        //Debug.Log("docRef : " + docRef);
        //// 이게 안됨

        //GPSQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        //{
        //    QuerySnapshot GPSQuerySnapshot = task.Result;
        //    foreach (DocumentSnapshot documentSnapshot in GPSQuerySnapshot.Documents)
        //    {
        //        Debug.Log(String.Format("Document data for {0} document:", documentSnapshot.Id));
        //        check = true;
        //    };
        //});
        return check;
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
        var dis = Math.Round(Vector3.Distance(pos, userPos), 2);
        return (float)dis;
    }

    //private string GetObstacleGPS(float userLat, float userLng, float x, float z)
    //{
    //    int radius = CompassBehaviour._compass;
    //    float disX = x * Mathf.Cos(radius);
    //    float disZ = z * Mathf.Sin(radius);

    //    float obLat = userLat + disX * 0.0001f;
    //    float obLng = userLng + disZ * 0.0001f;

    //    string gps = obLat.ToString() + "x" + obLng.ToString();

    //    return gps;
    //}

    private float Truncate(double n)
    {
        // 소수점 3자리 이하는 버림
        n = Math.Truncate(n * 1000) / 1000;
        return (float)n;
    }

}
