using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using System;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Threading.Tasks;
public class ClassificationObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    private int _obstaclePointNum;
    FirebaseFirestore db;
    private int criteria;
    public GameObject AlertPanel;
    private bool initDelay = false;
    private bool isSave = false;
    private DatabaseBehavior Database;

    void Start()
    {
        Database = new DatabaseBehavior();
        AlertPanel = GameObject.Find("AlertPanel");
        AlertPanel.SetActive(false);
        // criteria : 현재 프레임에서 feature point의 개수가 일정 이상일 때 장애물로 인식하기 위한 기준
        criteria = 20;
        db = FirebaseFirestore.DefaultInstance;
        StartCoroutine(InitDelay());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateData();
    }

    IEnumerator InitDelay()
    {
        Debug.Log("세션 시작");
        yield return new WaitForSeconds(5f);
        Debug.Log("5초 지남");
        initDelay = true;
    }

    IEnumerator DataDelay()
    {
        yield return new WaitForSeconds(3f);
        isSave = false;
    }

    private void UpdateData()
    {
        if (initDelay)
        {
            _obstaclePointNum = PointCloudVisualization._obstaclePoints.Count;

            if (_obstaclePointNum > criteria)
            {
                AlertPanel.SetActive(true);
                // 휴대폰 진동
                Handheld.Vibrate();
                if (!isSave)
                {
                    isSave = true;
                    Database.QueryData();
                    Database.SaveData();
                    StartCoroutine(DataDelay());
                }
                else
                {
                    Debug.Log("wait for 3 seconds!!");
                }
                //Debug.Log("장애물 찾음 !~~~");
            }
            else
            {
                AlertPanel.SetActive(false);
            }
        }
    }
}
