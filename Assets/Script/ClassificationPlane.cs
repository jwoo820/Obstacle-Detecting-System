using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ClassificationPlane : MonoBehaviour
{
    public ARPlane _ARPlane;
    // 색깔 변경
    public MeshRenderer _PlaneMeshRenderer;

    public TextMesh _TextMesh;

    public GameObject _Textobj;

    GameObject _mainCam;
    
    // Start is called before the first frame update
    void Start()
    {
        _mainCam = FindObjectOfType<Camera>().gameObject;   
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLabel();
        UpdatePlaneColor();
    }

    void UpdateLabel()
    {
        _TextMesh.text = _ARPlane.classification.ToString();
        _Textobj.transform.position = _ARPlane.center;
        _Textobj.transform.LookAt(_mainCam.transform);
        _Textobj.transform.Rotate(new Vector3(0, 180, 0));
    }


    void UpdatePlaneColor()
    {
        Color planeMatColor = Color.cyan;
        
        switch(_ARPlane.classification)
        {
            case PlaneClassification.None:
                planeMatColor = Color.cyan;
                //Debug.Log("None : " + _ARPlane.center);
                break;
            case PlaneClassification.Wall:
                planeMatColor = Color.white;
                //Debug.Log("Wall : " + _ARPlane.center);
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
        _PlaneMeshRenderer.material.color = planeMatColor;
    }

    void Ransac()
    {
        var c_max = 0;
        var c_cnt = 0;
        var T = 0.05;

    }
}