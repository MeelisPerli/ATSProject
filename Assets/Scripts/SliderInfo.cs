using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderInfo : MonoBehaviour
{

    public float MaxVal;

    private void Start() {
        UpdateSliderValue();
    }

    public void UpdateSliderValue() {
        GetComponentInChildren<Text>().text = Mathf.Round(GetComponent<Slider>().value * MaxVal).ToString();

    }
}
