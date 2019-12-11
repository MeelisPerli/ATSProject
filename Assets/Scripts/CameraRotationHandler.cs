using Mapbox.Unity.Location;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotationHandler : MonoBehaviour
    {

    private Text debugTF;
    public float sensitivity;
    public bool isOnManual;


    public Transform user;
    public Transform mapMarker;
    public float directionUpdateTime;
    private Vector3 userLastEuler;
    private Coroutine _autoDir;

    bool _isInitialized;

    void Start()
    {
        debugTF = GameObject.Find("DebugCanvas").GetComponentInChildren<Text>();
        Input.location.Start();
        if (!isOnManual) {
            DisplayOnDBW("Please move forward for around 20m");
            _autoDir = StartCoroutine(Automatic());
        }
    }

    // Update is called once per frame
    void Update()
    {
        Touch[] myTouches = Input.touches;
        
        if (Input.touchCount == 1 && isOnManual)
            Manual(myTouches);
    }

    private void Manual(Touch[] myTouches) {
        if (Screen.height / 2 < myTouches[0].position.y) {
            Vector3 offSet = transform.eulerAngles;
            offSet.y += myTouches[0].deltaPosition.x * Time.deltaTime * sensitivity;
            transform.eulerAngles = offSet;
        }
    }

    IEnumerator Automatic() {
        while (true) {
            if (userLastEuler != user.eulerAngles) {
                /*
                // user's direction
                float Uy = user.eulerAngles.y;
                // Marker direction from the user
                float My = Mathf.Rad2Deg * Mathf.Atan2(mapMarker.position.z - user.position.z, mapMarker.position.x - user.position.x) - 30; // the constant should depend on the longitute or latitute
                Vector3 eul = transform.eulerAngles;
                eul.y = Uy - My;
                transform.eulerAngles = eul;
                */
                //DisplayOnDBW(" User orientation: " + Uy+ " camera orientation: " + eul.y);
                DisplayOnDBW(" camera orientation: " + user.eulerAngles.y);

            }
            yield return new WaitForSeconds(directionUpdateTime);
        }
    }

    private void DisplayOnDBW(string text) {
        debugTF.text = text;
    }

    public void toggle() {
        isOnManual = !isOnManual;
        if (isOnManual) {
            StopCoroutine(_autoDir);
        } else {

            _autoDir = StartCoroutine(Automatic());
        }
    }
}
