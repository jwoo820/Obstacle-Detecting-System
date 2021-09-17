using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.Assertions;
public class GetObstacleData : MonoBehaviour
{
    [SerializeField] private string _obstaclePath = "obstacle_sheet/Info";


    public void GetData()
    {
        var firestore = FirebaseFirestore.DefaultInstance;

        firestore.Document(_obstaclePath).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Assert.IsNull(task.Exception);

            var obstacleData = task.Result.ConvertTo<ObstacleData>();

            Debug.Log("Latitude : " + obstacleData.Latitude);

            Debug.Log("Longitude : " + obstacleData.Longitude);
        });
    }

    private void Start()
    {
        GetData();
    }
}
