using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
//using GoogleARCore;

// 여기에 RANSAC 적용된 좌표 가져와서 평면 realtime 생성
public class ReferencePlane : MonoBehaviour
{
    public Plane _plane;

    public Camera _camera;

    public ARPoseDriver _arPoseDriver;

    public ARSessionOrigin _arSessionOrigin;

    private Vector3 _currPosition;
    

    Vector3 convert = new Vector3(0, 0, 0);
    Vector3 real;

    GameObject _mainCam;

    // Start is called before the first frame update
    void Start()
    {
        _mainCam = FindObjectOfType<Camera>().gameObject;



        //_arPoseDriver.
        
        //_camera = _arSessionOrigin.camera;
        //_camera.rotat
    }

    // Update is called once per frame
    void Update()
    {

        _currPosition = _arSessionOrigin.trackablesParent.localPosition;
        
        //Quaternion quaternion = Frame.Pose.rotation;
        //convert = quaternion.eulerAngles;
        //real = new Vector3(0, convert.y, 0);
        //transform.rotation = Quaternion.Euler(real);
        //transform.position = new Vector3(Frame.Pose.position.x, GroundDetection.real_y,
        //    Frame.Pose.position.z);

    }
}
