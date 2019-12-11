using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetLocTransformer : MonoBehaviour
{

    public GameObject target;
    public GameObject rlTarget;
    public Camera CoreDevice;
    public Transform user;
    public float Ratio;

    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveObject();
    }

    void moveObject() {
        
        float dist = Vector3.Distance(user.position, target.transform.position);
        float Uy = user.eulerAngles.y;
        float My = Mathf.Rad2Deg * Mathf.Atan2(target.transform.position.z - user.position.z, target.transform.position.x - user.position.x) - 30;
        float a = My - Uy;

        Vector3 pos = CoreDevice.transform.position;
        pos.x += dist * Ratio * slider.value * Mathf.Sin(Mathf.Deg2Rad * a);
        pos.z += dist * Ratio * slider.value * Mathf.Cos(Mathf.Deg2Rad * a);
        rlTarget.transform.position = pos;
        /*
        Vector3 pos = CoreDevice.transform.position;
        pos -= transform.position;
        pos += target.transform.position * Ratio*slider.value;
        pos.y = CoreDevice.transform.position.y;
        rlTarget.transform.position = pos;
        */
    }
}
