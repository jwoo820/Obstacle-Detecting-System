using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Threading;
using System;
public class ClassificationObstacle : MonoBehaviour
{
    public static int _obstaclePointNum;
    private int criteria;
    public GameObject AlertPanel;
    private bool initDelay = false;
    private bool isSave = false;
    private bool isQuery = true;
    private DatabaseBehavior Database;
    Image panelColor;

    void Start()
    {
        _obstaclePointNum = 0;
        try
        {
            Database = new DatabaseBehavior();
            AlertPanel = GameObject.Find("AlertPanel");
            panelColor = GameObject.Find("AlertPanel").GetComponent<Image>();
            AlertPanel.SetActive(false);
            // obstacle feature point criteria
            criteria = 20;
            StartCoroutine(InitDelay());
        }
        catch(NullReferenceException ex)
        {
            Debug.Log("NULL");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (initDelay)
        {
            _obstaclePointNum = PointCloudVisualization._obstaclePoints.Count;
            if(_obstaclePointNum > criteria)
            {
                SearchObstacle();
            }
            else
            {
                if (isQuery)
                {
                    StartCoroutine(SearchDB());
                }
                else
                {
                    //Debug.Log("Wait for 3 seconds");
                }
            }
        }
    }


    IEnumerator SearchDB()
    {
        Database.QueryData();
        isQuery = false;
        if (DatabaseBehavior.querySuccess)
        {
            panelColor.color = new Color(1, 0, 0, 0.5f);
            Handheld.Vibrate();
            AlertPanel.SetActive(true);
        }

        DatabaseBehavior.querySuccess = false;
        yield return new WaitForSeconds(3f);
        AlertPanel.SetActive(false);
        isQuery = true;
    }

    // init session delay time
    IEnumerator InitDelay()
    {
        Debug.Log("세션 시작");
        yield return new WaitForSeconds(5f);
        Debug.Log("5초 지남");
        initDelay = true;
    }
    // data save delay time
    IEnumerator SaveDataDelay()
    {
        yield return new WaitForSeconds(3f);
        AlertPanel.SetActive(false);
        isSave = false;
    }
    // save data to db
    private void SearchObstacle()
    {
        panelColor.color = new Color(1, 1, 0, 0.5f);
        AlertPanel.SetActive(true);
        Handheld.Vibrate();
        if (!isSave)
        {
            isSave = true;
            Database.SaveData();
            StartCoroutine(SaveDataDelay());
        }

        else
        {
            //Debug.Log("Data Save wait 3 seconds!!");
        }
    }
}