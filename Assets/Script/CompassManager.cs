using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CompassManager : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;
    public ARPlaneManager arPlaneManager;
    public GameObject rocketPrefab;
    public Button resetButton;

    private bool rocketCreated = false;
    private GameObject instantiatedRocket;

    private List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        resetButton.onClick.RemoveAllListeners();
        resetButton.onClick.AddListener(() =>
        {
            DeleteRocket(instantiatedRocket);
        });
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                if (touch.phase == TouchPhase.Ended)
                {
                    if (Input.touchCount == 1)
                    {
                        if (!rocketCreated)
                        {
                            //Rraycast Planes
                            if (arRaycastManager.Raycast(touch.position, arRaycastHits))
                            {
                                var pose = arRaycastHits[0].pose;
                                //CreateRocket(pose.position);
                                Vector3 spawnPos = Camera.main.transform.position;
                                spawnPos.y = spawnPos.y - 1f;
                                CreateRocket(spawnPos);
                                TogglePlaneDetection(false);
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
    private void CreateRocket(Vector3 position)
    {
        instantiatedRocket = Instantiate(rocketPrefab, position, Quaternion.identity);
        rocketCreated = true;
        resetButton.gameObject.SetActive(true);
    }

    private void TogglePlaneDetection(bool state)
    {
        foreach (var plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(state);
        }
        arPlaneManager.enabled = state;
    }


    public void DeleteRocket(GameObject cubeObject)
    {
        Destroy(cubeObject);
        resetButton.gameObject.SetActive(false);
        rocketCreated = false;
        TogglePlaneDetection(true);
    }
}
