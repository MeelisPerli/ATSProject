using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCamToUser : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Mapbox.Unity.Map.AbstractMap _map;

    [SerializeField]
    Button _button;

    [SerializeField]
    Transform _mapTransform;

    private void Start()
    {
        _button.onClick.AddListener(UpdateMapLocation);
    }

    private void UpdateMapLocation()
    {
        var location = Mapbox.Unity.Location.LocationProviderFactory.Instance.DefaultLocationProvider.CurrentLocation;
        _map.UpdateMap(location.LatitudeLongitude, _map.AbsoluteZoom);
        var playerPos = Camera.main.transform.position;
        _mapTransform.position = new Vector3(playerPos.x, _mapTransform.position.y, playerPos.z);
    }
}
