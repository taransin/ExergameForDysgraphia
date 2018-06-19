using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScaler : MonoBehaviour {

    public Slider innerSlider;
    public Slider outerSlider;
    public RectTransform inner;
    public RectTransform outer;

    private void Start()
    {
        outer.sizeDelta = new Vector2(217, 217);
        inner.sizeDelta = new Vector2(111, 111);
    }
    public void OnOuterSliderChange()
    {
        outer.sizeDelta = new Vector2(outerSlider.value, outerSlider.value);
    }
    public void OnInnerSliderChange()
    {
        inner.sizeDelta = new Vector2(innerSlider.value, innerSlider.value);
    }


}
