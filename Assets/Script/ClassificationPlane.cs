using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using System.Linq;
public class ClassificationPlane : MonoBehaviour
{
    public ARPlane _ARPlane;
    // 색깔 변경
    //public MeshRenderer _PlaneMeshRenderer;

    //public TextMesh _TextMesh;

    //public GameObject _Textobj;

    public LinkedList<float> _planeCenterList = new LinkedList<float>();
    // 20cm 이상 떨어졌을 때 obstacle point로 정ㅖ
    public static float _outlier = 0.15f;
    public static LinkedList<float> _yAxis = new LinkedList<float>();
    private static int _planeCenterCount = 100;
    List<float> distinctList;
    private Vector3 _planeCenter = new Vector3();
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

        UpdatePlaneCenter();
        ReferenceY();
        //Ransac();
    }

    //void UpdateLabel()
    //{
    //    _TextMesh.text = _ARPlane.classification.ToString();
    //    _Textobj.transform.position = _ARPlane.center;
    //    _Textobj.transform.LookAt(_mainCam.transform);
    //    _Textobj.transform.Rotate(new Vector3(0, 180, 0));
    //}

    void UpdatePlaneCenter()
    {
        // 지면 평면만 탐색하기
        if (_ARPlane.classification == PlaneClassification.Floor)
        {
            _planeCenter = _ARPlane.center;
            MaxPlaneCenterList(_planeCenter);
        }
        else
        {
            //Debug.Log("It's not a Floor");
        }

    }

    void MaxPlaneCenterList(Vector3 center)
    {
        if (!CheckRoi.PlaneCheck(center)) return;
        _planeCenterList.AddLast(center.y);
        
        //distinctList = _planeCenterList.Distinct().ToList();

        if (_planeCenterList.Count >= _planeCenterCount)
        {
            _planeCenterList.RemoveFirst();
        }

    }

    void UpdatePlaneColor()
    {
        Color planeMatColor = Color.cyan;

        switch (_ARPlane.classification)
        {
            case PlaneClassification.None:
                planeMatColor = Color.cyan;
                //Debug.Log("None : " + _ARPlane.center);
                break;
            case PlaneClassification.Wall:
                planeMatColor = Color.white;
                Debug.Log("Wall : " + _ARPlane.center);
                break;
            case PlaneClassification.Floor:
                planeMatColor = Color.green;
                //Debug.Log("Floor : " + _ARPlane.center);
                break;
            case PlaneClassification.Ceiling:
                planeMatColor = Color.blue;
                //Debug.Log("Ceiling : " + _ARPlane.center);
                break;
            case PlaneClassification.Table:
                planeMatColor = Color.yellow;
                //Debug.Log("Table : " + _ARPlane.center);
                break;
            case PlaneClassification.Seat:
                planeMatColor = Color.magenta;
                //Debug.Log("Seat : " + _ARPlane.center);
                break;
            case PlaneClassification.Door:
                //Debug.Log("Door : " + _ARPlane.center);
                planeMatColor = Color.red;
                break;
            case PlaneClassification.Window:
                //Debug.Log("Window : " + _ARPlane.center);
                planeMatColor = Color.clear;
                break;
        }
        planeMatColor.a = 0.33f;
        //_PlaneMeshRenderer.material.color = planeMatColor;
    }

    void Ransac()
    {
        double c_max = 0;
        double c_cnt = 0;
        float T = 0.05f;

        if (_planeCenterList.Count == 0) return;

        int y_cnt = _planeCenterList.Count;
        float tmp_y = 0f;
        //Debug.Log("Plane Center List : " + _planeCenterList.Count);
        foreach (var i in _planeCenterList)
        {
            Debug.Log("Current Plane : " + i);
            tmp_y = i;
            foreach (var j in _planeCenterList)
            {
                if (Math.Abs(tmp_y - j) <= T)
                {
                    c_cnt++;
                }
            }

            if (c_cnt > c_max)
            {
                c_max = c_cnt;
                _referenceY = tmp_y;
            }
            c_cnt = 0;
        }
    }

    void ReferenceY()
    {
        float tmp_y = 0f;
        if (_planeCenterList.Count == 0) return;

        foreach(var i in _planeCenterList)
        {
            tmp_y += i;
        }
        tmp_y /= _planeCenterList.Count;

        _referenceY = tmp_y;
        //Debug.Log("List Count : " + _planeCenterList.Count);
        //Debug.Log("reference Y : " + _referenceY);
    }
}