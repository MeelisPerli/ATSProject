using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCamToUser : MonoBehaviour
{


    [SerializeField]
    Camera cam;

    [SerializeField]
    Transform user;

    [SerializeField]
    Vector3 offset;

    public void UpdateMapLocation()
    {
        Vector3 pos = user.position + offset;
        pos.y = cam.transform.position.y;
        cam.transform.position = pos;
    }
}
