using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotationHandler : MonoBehaviour
    {

    private Text debugTF;
    public float sensitivity;

    void Start()
    {
        debugTF = GameObject.Find("DebugCanvas").GetComponentInChildren<Text>();

        Input.location.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Touch[] myTouches = Input.touches;
        if (Input.touchCount == 1)
        {
            if (Screen.height/2 < myTouches[0].position.y)
            {
                Vector3 offSet = transform.eulerAngles;
                offSet.y += myTouches[0].deltaPosition.x * Time.deltaTime * sensitivity;

                transform.eulerAngles = offSet;
            }
        }
    }

    private void DisplayOnDBW(string text) {
        debugTF.text = text;
    }
}
