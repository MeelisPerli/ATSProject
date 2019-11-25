using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotationHandler : MonoBehaviour
{

    private Text debugTF;
    private float directions;
    public float avgOverTime;
    private float timeCounter;
    public float offset;
    private int count;

    private float lastDir;
    private float newDir;



    void Start()
    {
        debugTF = GameObject.Find("DebugCanvas").GetComponentInChildren<Text>();
        timeCounter = Time.time + avgOverTime;
        directions = 0;
        lastDir = 0;
        newDir = 0;
        Input.location.Start();
    }

    // Update is called once per frame
    void Update()
    {
        directions += Input.compass.magneticHeading;
        count++;
        if (timeCounter <= Time.time) {
            lastDir = newDir;
            newDir = directions / count;
            

            timeCounter = Time.time + avgOverTime;
            directions = 0;
            count = 0;
        }
        if (lastDir != newDir) {
            float t = timeCounter - Time.time;
            transform.rotation = Quaternion.Euler(0, Mathf.LerpAngle(lastDir, newDir, 1 - t / avgOverTime), 0);
            DisplayOnDBW("a" + (1 - t / avgOverTime));
        }
    }



    private void DisplayOnDBW(string text) {
        debugTF.text = text;
    }
}
