using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Threading;
public class ClassificationObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    private int _obstaclePointNum;
    private int criteria;
    public GameObject AlertPanel;
    private bool initDelay = false;
    private bool isSave = false;
    private bool isQuery = false;
    private DatabaseBehavior Database;

    Image panelColor;
    void Start()
    {
        Database = new DatabaseBehavior();
        AlertPanel = GameObject.Find("AlertPanel");
        panelColor = GameObject.Find("AlertPanel").GetComponent<Image>();
        AlertPanel.SetActive(false);
        // criteria : 현재 프레임에서 feature point의 개수가 일정 이상일 때 장애물로 인식하기 위한 기준
        criteria = 20;
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

    IEnumerator SaveDataDelay()
    {
        yield return new WaitForSeconds(3f);
        isSave = false;
    }

    IEnumerator QueryDataDelay()
    {
        yield return new WaitForSeconds(3f);
        isQuery = false;
    }

    private void UpdateData()
    {
        if (initDelay)
        {
            // 1. db에 데이터 있나 없나 검색
            // 2. 있으면 호출 아니면 pointcloud 계산
            //Debug.Log("QueryData : " + Database.QueryData());

            var queryCheck =  Database.QueryData();

            Debug.Log("test 4");


            //if (Database.QueryData()) queryCheck = true;

            //Debug.Log("task5 : " + queryCheck);
            if (queryCheck)
            {
                panelColor.color = new Color(1, 0, 0, 0.5f);
                AlertPanel.SetActive(true);
                Handheld.Vibrate();
                if (!isQuery)
                {
                    isQuery = true;
                    Debug.Log("Delay 3seconds");
                    StartCoroutine(QueryDataDelay());
                    AlertPanel.SetActive(false);
                }
                else
                {
                    Debug.Log("Query wait 3 seconds!!");
                }
            }
            else
            {
                _obstaclePointNum = PointCloudVisualization._obstaclePoints.Count;
                if (_obstaclePointNum > criteria)
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
                }
                else
                {
                    AlertPanel.SetActive(false);
                }
            }
        }
    }
}