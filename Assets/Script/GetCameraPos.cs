using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCameraPos : MonoBehaviour
{
    public Camera _camera;

    public static Vector3 _userPos;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        _userPos = _camera.transform.position;
        //Debug.Log("x : " + _userPos.x + ", y : " + _userPos.y + ", z : " + _userPos.z);
    }
}
