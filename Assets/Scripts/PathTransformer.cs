using Mapbox.Unity.MeshGeneration.Factories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathTransformer : MonoBehaviour
{

    public Camera coreDevice;
    public float ratio;
    public Slider slider;

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
            ARPath.transform.localScale = new Vector3(33, 33, 33);

            // updating loc
            Vector3 pos = coreDevice.transform.position;
            pos -= transform.position;
            pos += pathMesh.transform.position * ratio * slider.value;
            pos.y = coreDevice.transform.position.y - 130;
            ARPath.transform.position = pos;
            yield return new WaitForSeconds(1f);
        }
        ARPath.Destroy();
        ARPath = null;
    }
}
