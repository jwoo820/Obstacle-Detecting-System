using UnityEngine;
using Firebase.Firestore;

[FirestoreData]

public struct ObstacleData
{
    [FirestoreProperty]

    public double Latitude { get; set; }

    [FirestoreProperty]

    public double Longitude { get; set; }

}
