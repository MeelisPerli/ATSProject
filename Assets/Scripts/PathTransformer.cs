using Mapbox.Unity.MeshGeneration.Factories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathTransformer : MonoBehaviour
{

    public Camera coreDevice;
    public float ratio;
    public float maxSize;
    public Slider distslider;
    public Slider sizeSlider;
    public Transform user;


    private GameObject pathMesh;
    private DirectionsFactory df;
    private GameObject ARPath;

    private void Start() {
        df = GetComponent<DirectionsFactory>();
    }

    void Update()
    {
        pathMesh = df.getPathObject();
        if (pathMesh != null) {
            if (ARPath == null)
                StartCoroutine(createARPath());
        }
    }


    IEnumerator createARPath() {
        while (pathMesh != null) {
            // creating new object
            if (ARPath != null) {
                ARPath.Destroy();
            }
            ARPath = Instantiate(pathMesh);
            ARPath.transform.localScale = new Vector3(sizeSlider.value*maxSize, sizeSlider.value * maxSize, sizeSlider.value * maxSize);

            // updating loc
            float dist = Vector3.Distance(user.position, ARPath.transform.position);
            float Uy = user.eulerAngles.y;
            float My = Mathf.Rad2Deg * Mathf.Atan2(ARPath.transform.position.z - user.position.z, ARPath.transform.position.x - user.position.x) - 30;
            float a = My - Uy;

            Vector3 pos = coreDevice.transform.position;
            pos.x += dist * ratio * distslider.value * Mathf.Sin(Mathf.Deg2Rad * a);
            pos.z += dist * ratio * distslider.value * Mathf.Cos(Mathf.Deg2Rad * a);
            ARPath.transform.position = pos;

            /*
            Vector3 pos = coreDevice.transform.position;
            pos -= transform.position;
            pos += pathMesh.transform.position * ratio * distslider.value;
            pos.y = coreDevice.transform.position.y - 130;
            ARPath.transform.position = pos;
            */
            yield return new WaitForSeconds(1f);
            
        }
        ARPath.Destroy();
        ARPath = null;
    }
}
