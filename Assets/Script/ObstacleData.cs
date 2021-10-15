using UnityEngine;
using Firebase.Firestore;

[FirestoreData]

public struct ObstacleData
{
    [FirestoreProperty]

    public float Latitude { get; set; }

    [FirestoreProperty]

    public float Longitude { get; set; }

    [FirestoreProperty]

    public float position_x { get; set; }

    [FirestoreProperty]

    public float position_z { get; set; }

    [FirestoreProperty]

    public string compass { get; set; }

    [FirestoreProperty]

    public float obstacleDis { get; set; }

    [FirestoreProperty]

    public string obGPS { get; set; }

}
