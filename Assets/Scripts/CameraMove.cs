using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public Transform user;
    private Camera cam;
    public Vector3 defaultOffSet;
    public float sensitivity;
    private Vector3 currentOffSet;
    public bool wasClickedOnObject;
    private int layerMask;

    void Start()
    {
        currentOffSet = defaultOffSet;
        cam = GetComponent<Camera>();
        layerMask = 1 << 9;
        layerMask = ~layerMask;
    }

    private void Update() {
        // Movement with WASD keys (just for debugging)
        if (Input.GetKey("w")) {
            currentOffSet.z += sensitivity * Time.deltaTime * 10;
        }
        if (Input.GetKey("s")) {
            currentOffSet.z -= sensitivity * Time.deltaTime * 10;
        }
        if (Input.GetKey("d")) {
            currentOffSet.x += sensitivity * Time.deltaTime * 10;
        }
        if (Input.GetKey("a")) {
            currentOffSet.x -= sensitivity * Time.deltaTime * 10;
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
                    currentOffSet -= new Vector3(offSet.x, 0, offSet.y) * Time.deltaTime * sensitivity;
                }
                // Do something with the object that was hit by the raycast.
            }
            
        }

        transform.position = currentOffSet;
        wasClickedOnObject = false;
    }
}
