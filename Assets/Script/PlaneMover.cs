using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMover : MonoBehaviour
{
    GameObject _mainCam;
    Vector3 _convert = new Vector3(0, 0, 0);
    Vector3 _real;
    // Start is called before the first frame update
    void Start()
    {
        _mainCam = GameObject.Find("AR Camera");
        if (_mainCam != null)
        {
            Debug.Log("카메라 찾기 성공");
        }
        else
        {
            Debug.Log("카메라 찾기 실패");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion quaternion = _mainCam.transform.rotation;
        _convert = quaternion.eulerAngles;

        _real = new Vector3(0, _convert.y, 0);
        transform.rotation = Quaternion.Euler(_real);
        Debug.Log("Reference Y axis : " + ClassificationPlane._referenceY);
        transform.position = new Vector3(_mainCam.transform.position.x, ClassificationPlane._referenceY, _mainCam.transform.position.z);
    }
}
