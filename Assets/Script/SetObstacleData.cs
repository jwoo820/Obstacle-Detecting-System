using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

public class SetObstacleData : MonoBehaviour
{
    [SerializeField] private string _obstaclePath = "obstacle_sheet/Info";

    FirebaseFirestore _firestore;

    ObstacleData _obstacledata;
    
    void Start()
    {
        double lat = GpsManager.current_Lat;
        double lng = GpsManager.current_Long;
        _obstacledata = new ObstacleData
        {
            Latitude = lat,
            Longitude = lng,
        };
        _firestore = FirebaseFirestore.DefaultInstance;

        SetData();
    }

    private void Update()
    {

    }

    void SetData()
    {
        if (_firestore != null)
        {
            Debug.Log("firestore success");
            _firestore.Document(_obstaclePath).SetAsync(_obstacledata);
        }

        else
        {
            Debug.Log("firestore fail");
        }
    }
}
