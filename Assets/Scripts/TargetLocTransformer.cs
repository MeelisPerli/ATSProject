using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetLocTransformer : MonoBehaviour
{

    public GameObject target;
    public GameObject rlTarget;
    public Camera coreDevice;
    public float ratio;

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

        Vector3 pos = coreDevice.transform.position;
        pos.x = target.transform.position.x * ratio * slider.value;
        pos.z = target.transform.position.z * ratio * slider.value;
        rlTarget.transform.position = pos;
        /*
        float dist = Vector3.Distance(user.position, target.transform.position);
        float Uy = user.eulerAngles.y;
        float My = Mathf.Rad2Deg * Mathf.Atan2(target.transform.position.z - user.position.z, target.transform.position.x - user.position.x) - 30;
        float a = Uy - My;

        Vector3 pos = CoreDevice.transform.position;
        pos.x += dist * Ratio * slider.value * Mathf.Sin(Mathf.Deg2Rad * a);
        pos.z += dist * Ratio * slider.value * Mathf.Cos(Mathf.Deg2Rad * a);
        rlTarget.transform.position = pos; 
        */


    }
}
