using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
public class ClassificationPlane : MonoBehaviour
{
    public ARPlane _ARPlane;
    // 색깔 변경
    public MeshRenderer _PlaneMeshRenderer;

    public TextMesh _TextMesh;

    public GameObject _Textobj;

    public LinkedList<Vector3> _planeCenterList = new LinkedList<Vector3>();
    public static float _outlier = 0.2f;
    public static LinkedList<float> _yAxis = new LinkedList<float>();
    private static int _planeCenterCount = 100;
    public Vector3 _planeCenter = new Vector3();
    public static float _referenceY = 0;
    GameObject _mainCam;

    // Start is called before the first frame update
    void Start()
    {
        _mainCam = FindObjectOfType<Camera>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateLabel();
        //UpdatePlaneColor();
        
        Ransac();
        UpdatePlaneCenter();
    }

    void UpdateLabel()
    {
        _TextMesh.text = _ARPlane.classification.ToString();
        _Textobj.transform.position = _ARPlane.center;
        _Textobj.transform.LookAt(_mainCam.transform);
        _Textobj.transform.Rotate(new Vector3(0, 180, 0));
    }

    void UpdatePlaneCenter()
    {
        _planeCenter = _ARPlane.center;
        MaxPlaneCenterList(_planeCenter);
    }

    void MaxPlaneCenterList(Vector3 center)
    {
        if (!CheckRoi.Check(center)) return;
        _planeCenterList.AddLast(center);
        if(_planeCenterList.Count >= _planeCenterCount)
        {
            _planeCenterList.RemoveFirst();
        }
    }

    void UpdatePlaneColor()
    {
        Color planeMatColor = Color.cyan;

        switch(_ARPlane.classification)
        {
            case PlaneClassification.None:
                planeMatColor = Color.cyan;
                Debug.Log("None : " + _ARPlane.center);
                break;
            case PlaneClassification.Wall:
                planeMatColor = Color.white;
                Debug.Log("Wall : " + _ARPlane.center);
                break;
            case PlaneClassification.Floor:
                planeMatColor = Color.green;
                Debug.Log("Floor : " + _ARPlane.center);
                break;
            case PlaneClassification.Ceiling:
                planeMatColor = Color.blue;
                Debug.Log("Ceiling : " + _ARPlane.center);
                break;
            case PlaneClassification.Table:
                planeMatColor = Color.yellow;
                Debug.Log("Table : " + _ARPlane.center);
                break;
            case PlaneClassification.Seat:
                planeMatColor = Color.magenta;
                Debug.Log("Seat : " + _ARPlane.center);
                break;
            case PlaneClassification.Door:
                Debug.Log("Door : " + _ARPlane.center);
                planeMatColor = Color.red;
                break;
            case PlaneClassification.Window:
                Debug.Log("Window : " + _ARPlane.center);
                planeMatColor = Color.clear;
                break;
        }
        planeMatColor.a = 0.33f;
        _PlaneMeshRenderer.material.color = planeMatColor;
    }

    void Ransac()
    {
        double c_max = 0;
        double c_cnt = 0;
        float T = 0.05f;

        if (_planeCenterList.Count == 0) return;

        int y_cnt = _planeCenterList.Count;
        float tmp_y = 0;

        foreach(Vector3 i in _planeCenterList)
        {
            tmp_y = i.y;
            foreach(Vector3 j in _planeCenterList)
            {
                if(Math.Abs(tmp_y - j.y) <= T)
                {
                    c_cnt++;
                }
            }

            if(c_cnt > c_max)
            {
                c_max = c_cnt;
                _referenceY = tmp_y;
            }

            c_cnt = 0;
        }
        //Debug.Log("reference Y : " + _referenceY);
    }
}