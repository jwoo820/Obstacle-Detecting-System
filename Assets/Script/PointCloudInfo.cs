using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PointCloudInfo : MonoBehaviour
{
    // The AR Foundation PointCloud script
    private ARPointCloud _arPointCloud;
    private ARSessionOrigin _arSessionOrigin;
    public ARPointCloudManager _pointCloudManager;

    int totalNumber;
    List<Vector3> featurePoints = new List<Vector3>();

    // Reference to logging UI element in the canvas
    public UnityEngine.UI.Text Log;

    private void Start()
    {
        _arSessionOrigin = GetComponent<ARSessionOrigin>();

        _arPointCloud = _arSessionOrigin.trackablesParent.GetComponentInChildren<ARPointCloud>();
    }

    void OnEnable()
    {
        // Subscribe to event when point cloud changed
        _arPointCloud = GetComponent<ARPointCloud>();
        _arPointCloud.updated += OnPointCloudChanged;

        _pointCloudManager.pointCloudsChanged += PointCloudManager_pointCloudsChanged;
    }

    void OnDisable()
    {
        // Unsubscribe event when this element is disabled
        _arPointCloud.updated -= OnPointCloudChanged;
    }

    private void Update()
    {
        _arPointCloud = _arSessionOrigin.trackablesParent.GetComponent<ARPointCloud>();
        featurePoints = new List<Vector3>(_arPointCloud.positions);
        totalNumber = featurePoints.Count;

        Debug.Log("Count : " + totalNumber);
        foreach (var i in featurePoints)
        {
            Debug.Log("Position : " + i);
        }

    }

    private void PointCloudManager_pointCloudsChanged(ARPointCloudChangedEventArgs obj)
    {
        List<ARPoint> addedPoints = new List<ARPoint>();
        foreach(var pointCloud in obj.added)
        {
            foreach(var pos in pointCloud.positions)
            {
                ARPoint newPoint = new ARPoint(pos);
                Debug.Log("Added Point : " + pos);
                addedPoints.Add(newPoint);
            }

        }

        List<ARPoint> updatedPoints = new List<ARPoint>();
        foreach(var pointCloud in obj.updated)
        {
            foreach(var pos in pointCloud.positions)
            {
                ARPoint newPoint = new ARPoint(pos);
                Debug.Log("Updated Point : "+ pos);
                updatedPoints.Add(newPoint);
            }
        }

    }


    private void OnPointCloudChanged(ARPointCloudUpdatedEventArgs eventArgs)
    {



        if (!_arPointCloud.positions.HasValue ||
            !_arPointCloud.identifiers.HasValue ||
            !_arPointCloud.confidenceValues.HasValue)
            return;

        var positions = _arPointCloud.positions.Value;
        var identifiers = _arPointCloud.identifiers.Value;
        var confidence = _arPointCloud.confidenceValues.Value;

        if (positions.Length == 0) return;

        var logText = "Number of points: " + positions.Length + "\nPoint info: x = "
                   + positions[0].x + ", y = " + positions[0].y + ", z = " + positions[0].z
                   + ",\n Identifier = " + identifiers[0] + ", Confidence = " + confidence[0];

        if (Log)
        {
            Log.text = logText;
        }
        else
        {
            Debug.Log(logText);
        }
    }
}

public class ARPoint
{
    public float x;
    public float y;
    public float z;

    public ARPoint(Vector3 pos)
    {
        x = pos.x;
        y = pos.y;
        z = pos.z;
    }
}