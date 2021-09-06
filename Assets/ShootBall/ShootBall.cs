using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBall : MonoBehaviour
{
    public GameObject _theBall;
    public Transform _camObj;
    public Transform _shootPoint;
    // Start is called before the first frame update
    public void ShootBallObj()
    {
        GameObject tObj = Instantiate(_theBall);
        tObj.transform.position = _shootPoint.transform.position;
        Vector3 tVec = (_shootPoint.transform.position - _camObj.transform.position).normalized;

        Rigidbody tR = tObj.GetComponent<Rigidbody>();

        tR.AddForce(tVec * 100f);
    }
}
