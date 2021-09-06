using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GpsManager : MonoBehaviour
{
    public static double first_Lat;
    public static double first_Long;
    public static double current_Lat;
    public static double current_Long;

    private static WaitForSeconds second;

    private static bool gpsStarted = false;

    private static LocationInfo location;

    private void Awake()
    {
        second = new WaitForSeconds(1.0f);
    }

    IEnumerator Start()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("GPS is not enabled");
            yield break;
        }
        Input.location.Start();
        Debug.Log("Awaiting initialization");

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return second;
            maxWait -= 1;
        }
        if (maxWait < 1)
        {
            Debug.Log("Time out");
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unabled to determine device location");
            yield break;
        }
        else
        {
            location = Input.location.lastData;
            first_Lat = location.latitude * 1.0d;
            first_Long = location.longitude * 1.0d;
            gpsStarted = true;

            while (gpsStarted)
            {
                location = Input.location.lastData;
                current_Lat = location.latitude * 1.0d;
                current_Long = location.longitude * 1.0d;
                Debug.Log("latitude : " + current_Lat + "longitude : " + current_Long);
                yield return second;
            }
        }

    }

    public static void StopGps()
    {
        if (Input.location.isEnabledByUser)
        {
            gpsStarted = false;
            Input.location.Stop();
        }
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }
}