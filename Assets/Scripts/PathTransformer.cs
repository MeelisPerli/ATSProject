using Mapbox.Unity.MeshGeneration.Factories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathTransformer : MonoBehaviour
{

    public Camera coreDevice;
    public float maxSize;
    public Slider sizeSlider;
    public Transform ARmarker;
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
            //size
            ARPath.transform.localScale = new Vector3(sizeSlider.value*maxSize, sizeSlider.value * maxSize, sizeSlider.value * maxSize);
            //pos
            Vector3 pos = coreDevice.transform.position;
            pos.y = coreDevice.transform.position.y - 200;
            pos.x = 0;
            pos.z = 0;
            ARPath.transform.position = pos;
            //rot
            //ARPath.transform.LookAt(ARmarker.position, Vector3.up);
            
            
            /*
            */
            yield return new WaitForSeconds(0.5f);
            
        }
        ARPath.Destroy();
        ARPath = null;
    }
}
