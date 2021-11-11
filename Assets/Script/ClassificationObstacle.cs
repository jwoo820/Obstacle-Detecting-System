using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Threading;

public class ClassificationObstacle : MonoBehaviour
{
    private int _obstaclePointNum;
    private int criteria;
    public GameObject AlertPanel;
    private bool initDelay = false;
    private bool isSave = false;
    private bool isQuery = true;
    private DatabaseBehavior Database;
    Image panelColor;

    void Start()
    {
        Database = new DatabaseBehavior();
        AlertPanel = GameObject.Find("AlertPanel");
        panelColor = GameObject.Find("AlertPanel").GetComponent<Image>();
        AlertPanel.SetActive(false);
        // criteria : 현재 프레임에서 feature point의 개수가 일정 이상일 때 장애물로 인식하기 위한 기준
        criteria = 30;
        StartCoroutine(InitDelay());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateData();
    }

    // 초기 5초 동안은 장애물 탐지 X 
    IEnumerator InitDelay()
    {
        Debug.Log("세션 시작");
        yield return new WaitForSeconds(5f);
        Debug.Log("5초 지남");
        initDelay = true;
    }

    // 한번 저장하면 3초 대기 후 저장 
    IEnumerator SaveDataDelay()
    {
        yield return new WaitForSeconds(3f);
        isSave = false;
    }
    // 디비를 3초에 한번 씩 읽기
    IEnumerator QueryDataDelay()
    {
        yield return new WaitForSeconds(3f);
        isQuery = false;
    }

    private void UpdateData()
    {
        if (initDelay)
        {
            _obstaclePointNum = PointCloudVisualization._obstaclePoints.Count;
           
            if (isQuery)
            {
                panelColor.color = new Color(1, 0, 0, 0.5f);
                AlertPanel.SetActive(true);
                Handheld.Vibrate();
                StartCoroutine(QueryDataDelay());

                Database.QueryData();
                StartCoroutine(QueryDataDelay());
                AlertPanel.SetActive(false);
            }
            else if (_obstaclePointNum > criteria)
            {
                panelColor.color = new Color(1, 1, 0, 0.5f);
                AlertPanel.SetActive(true);
                // 휴대폰 진동
                Handheld.Vibrate();
                if (!isSave)
                {
                    isSave = true;
                    Database.SaveData();
                    StartCoroutine(SaveDataDelay());
                }
                else
                {
                    Debug.Log("Data Save wait 3 seconds!!");
                }
                AlertPanel.SetActive(false);
            }
        }
    }
}