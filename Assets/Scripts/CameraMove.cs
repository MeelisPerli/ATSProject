using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public Transform user;
    public Vector3 defaultOffSet;
    public float sensitivity;
    private Vector3 currentOffSet;
    public bool wasClickedOnObject;

    void Start()
    {
        currentOffSet = defaultOffSet;

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

        if (Input.touchCount == 1 && !wasClickedOnObject) {
            Vector2 offSet = myTouches[0].deltaPosition;
            if (offSet.magnitude > 0.2) {
                currentOffSet -= new Vector3(offSet.x, 0, offSet.y) * Time.deltaTime * sensitivity;
            }
        }

        transform.position = currentOffSet;
        wasClickedOnObject = false;
    }
}
