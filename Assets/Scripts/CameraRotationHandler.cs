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

    public float distanceDeltaReq;
    public Transform user;
    public float directionUpdateTime;
    private Vector2d pos1;
    private Vector2d pos2;
    private Coroutine _autoDir;

    bool _isInitialized;

    ILocationProvider _locationProvider;
    ILocationProvider LocationProvider {
        get {
            if (_locationProvider == null) {
                _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
            }

            return _locationProvider;
        }
    }

    void Start()
    {
        LocationProviderFactory.Instance.mapManager.OnInitialized += () => _isInitialized = true;
        debugTF = GameObject.Find("DebugCanvas").GetComponentInChildren<Text>();
        pos1 = new Vector2d(0, 0);
        Input.location.Start();
        if (!isOnManual) {
            DisplayOnDBW("Please move forward for around 10m");
            //_autoDir = StartCoroutine(Automatic());
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
        int c = 0;
        while (true) {
            
            //DisplayOnDBW("dist: " + Vector3.Distance(pos1, pos2));
            if (_isInitialized) {
                if (pos1.x == 0 && pos1.y == 0) {
                    pos1 = LocationProvider.CurrentLocation.LatitudeLongitude;
                }
                else {
                    c += 1;
                    pos2 = LocationProvider.CurrentLocation.LatitudeLongitude;
                    double dist = 50000*Vector2d.Distance(pos1, pos2);
                    if (dist > distanceDeltaReq) {
                        float dx = (float)(pos1.x - pos2.x);
                        float dy = (float)(pos1.y - pos2.y);
                        float ori = -Mathf.Atan2(dx, dy) * Mathf.Rad2Deg;
                        Vector3 rot = user.transform.eulerAngles;
                        rot.y = ori;
                        //user.transform.eulerAngles = rot;
                        GetComponentInChildren<Camera>().transform.eulerAngles = new Vector3();
                        transform.forward = rot;
                        DisplayOnDBW("ori: " + rot.y + " dist: " + dist + " c: " + c);
                        pos1 = pos2;

                    }
                    //DisplayOnDBW(" dist: " + dist + " c: " + c);
                }
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
            //_autoDir = StartCoroutine(Automatic());
        }
    }
}
