using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections;
using System.Collections.Generic;
public class PointCloudInfo : MonoBehaviour
{
    public ARSessionOrigin _arSessionOrigin;
    public ARPointCloud _arPointCloud;

    int _totalNum;
    LinkedList<Vector3> _featurePoints;

    private void Start()
    {
        _arSessionOrigin = GetComponent<ARSessionOrigin>();

        _arPointCloud = _arSessionOrigin.trackablesParent.GetComponentInChildren<ARPointCloud>();
    }

    private void Update()
    {
        _arPointCloud = _arSessionOrigin.trackablesParent.GetComponentInChildren<ARPointCloud>();
        _featurePoints = new LinkedList<Vector3>(_arPointCloud.positions);
        if(_featurePoints.Count == 0)
        {
            return;
        }
        //foreach (Vector3 i in _featurePoints)
        //{
        //    Debug.Log("Point Cloud Position : " + i);
        //}
        _totalNum = _featurePoints.Count;

        //Debug.Log("Point Cloud Count : " + _totalNum);
    }
}