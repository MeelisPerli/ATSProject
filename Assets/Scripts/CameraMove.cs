using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{

    public Transform user;
    private Camera cam;
    public float sensitivity;
    public bool wasClickedOnObject;
    private int layerMask;
    private Vector3 _deltaVec;

    void Start()
    {
        cam = GetComponent<Camera>();
        layerMask = 1 << 9;
        layerMask = ~layerMask;
    }


    private void Update() {
        // Movement with WASD keys (just for debugging)
        _deltaVec.x = 0;
        _deltaVec.y = 0;
        _deltaVec.z = 0;
        if (Input.GetKey("w")) {
            _deltaVec.z += sensitivity * Time.deltaTime * 10;
        }
        if (Input.GetKey("s")) {
            _deltaVec.z -= sensitivity * Time.deltaTime * 10;
        }
        if (Input.GetKey("d")) {
            _deltaVec.x += sensitivity * Time.deltaTime * 10;
        }
        if (Input.GetKey("a")) {
            _deltaVec.x -= sensitivity * Time.deltaTime * 10;
        }
    }

    private void LateUpdate() {
        Touch[] myTouches = Input.touches;

        if (Input.touchCount == 1 && !wasClickedOnObject && Screen.height / 2 > myTouches[0].position.y) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(myTouches[0].position);
            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                Vector2 offSet = myTouches[0].deltaPosition;
                if (offSet.magnitude > 0.2) {
                    _deltaVec -= new Vector3(offSet.x, 0, offSet.y) * Time.deltaTime * sensitivity;
                }
                // Do something with the object that was hit by the raycast.
            }
            
        }

        transform.position += _deltaVec;
        wasClickedOnObject = false;
    }
}
