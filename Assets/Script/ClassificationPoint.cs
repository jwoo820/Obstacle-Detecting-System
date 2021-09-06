using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections;
public class ClassificationPoint : MonoBehaviour
{
    public ARPointCloud _ARPointCloud;

    public ARPointCloudManager _ARPointCloudManager;
    //public MeshRenderer _PointMeshRenderer;

    private void OnEnable()
    {
        _ARPointCloudManager.pointCloudsChanged += GetPointPosition;
    }

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    void GetPointPosition(ARPointCloudChangedEventArgs obj)
    {
        List<ARPoint> addedPoints = new List<ARPoint>();
        foreach(var pointCloud in obj.added)
        {
            foreach(var pos in pointCloud.positions)
            {
                ARPoint newPoint = new ARPoint(pos);
                addedPoints.Add(newPoint);
            }
        }

        List<ARPoint> updatedPoints = new List<ARPoint>();
        foreach(var pointCloud in obj.updated)
        {
            foreach(var pos in pointCloud.positions)
            {
                ARPoint newPoint = new ARPoint(pos);
                updatedPoints.Add(newPoint);
            }
            foreach(var pos in updatedPoints)
            {
                Debug.Log(pos);
            }
        }
    }

}

public class ARPoint1
{
    public float x;
    public float y;
    public float z;

    public ARPoint1(Vector3 pos)
    {
        x = pos.x;
        y = pos.y;
        z = pos.z;
    }
}