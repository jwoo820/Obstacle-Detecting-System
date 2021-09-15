using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

public class SetObstacleData : MonoBehaviour
{
    [SerializeField] private string _obstaclePath = "obstacle_sheet/Info";

    void Start()
    {
        var obstacleData = new ObstacleData
        {
            Latitude = GpsManager.current_Lat,
            Longitude = GpsManager.current_Long
        };
        var firestore = FirebaseFirestore.DefaultInstance;

        if (firestore != null)
        {
            Debug.Log("firestore success");
            firestore.Document(_obstaclePath).SetAsync(obstacleData);
        }

        else
        {
            Debug.Log("firestore fail");
        }
    }

    private void Update()
    {
        if (GpsManager.current_Lat == 0)
        {
            return;
        }
        else if (GpsManager.current_Long == 0)
        {
            return;
        }

        else
        {
            var obstacleData = new ObstacleData
            {
                Latitude = GpsManager.current_Lat,
                Longitude = GpsManager.current_Long
            };
            var firestore = FirebaseFirestore.DefaultInstance;

            if (firestore != null)
            {
                Debug.Log("firestore success");
                firestore.Document(_obstaclePath).SetAsync(obstacleData);
            }

            else
            {
                Debug.Log("firestore fail");
            }
        }
    }
}
