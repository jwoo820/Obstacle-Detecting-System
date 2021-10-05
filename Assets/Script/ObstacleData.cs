using UnityEngine;
using Firebase.Firestore;

[FirestoreData]

public struct ObstacleData
{
    [FirestoreProperty]

    public double Latitude { get; set; }

    [FirestoreProperty]

    public double Longitude { get; set; }

    [FirestoreProperty]

    public float position_x { get; set; }

    [FirestoreProperty]

    public float position_y { get; set; }

    [FirestoreProperty]

    public float position_z { get; set; }

    [FirestoreProperty]

    public float rotation_x { get; set; }

    [FirestoreProperty]

    public float rotation_y { get; set; }

    [FirestoreProperty]

    public float rotation_z { get; set; }

    [FirestoreProperty]

    public string compass { get; set; }

}
