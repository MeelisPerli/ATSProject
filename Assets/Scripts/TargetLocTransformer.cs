using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetLocTransformer : MonoBehaviour
{

    public GameObject target;
    public GameObject rlTarget;
    public Camera CoreDevice;
    public float Ratio;


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
        Vector3 pos = CoreDevice.transform.position;
        pos -= transform.position;
        pos += target.transform.position * Ratio;
        pos.y = CoreDevice.transform.position.y;
        rlTarget.transform.position = pos;
    }
}
