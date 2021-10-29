using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CompassText : MonoBehaviour
{
    private Text Compass;
    // Start is called before the first frame update
    void Start()
    {
        Compass = GameObject.Find("InfoText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        SetText();
    }

    void SetText()
    {
        Compass.text = CompassBehaviour.curr_compass
            + "\n Latitude : "
            + GpsManager.current_Lat
            +"\n Longitude : "
            + GpsManager.current_Long;
    }
}
