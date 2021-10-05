using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCameraPos : MonoBehaviour
{
    public Camera _camera;

    private Gyroscope _gyroscope;
    private bool _gyroEnabled;
    public static Vector3 _userPos;
    public static Quaternion _userRot;
    private Quaternion rot;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        _gyroEnabled = EnabledGyro();
    }

    private bool EnabledGyro()
    {
        if(SystemInfo.supportsGyroscope)
        {
            _gyroscope = Input.gyro;
            _gyroscope.enabled = true;

            rot = new Quaternion(0, 0, 1, 0);
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        _userPos = _camera.transform.position;

        if(_gyroEnabled)
        {
            _userRot = _gyroscope.attitude * rot;
            //Debug.Log("rot" + _gyroscope.attitude * rot);
        }
        //_angleGyro = _gyroscope.rotationRate;
        //Debug.Log("Camera Pose - x : " + _userPos.x + ", y : " + _userPos.y + ", z : " + _userPos.z);
    }
}
