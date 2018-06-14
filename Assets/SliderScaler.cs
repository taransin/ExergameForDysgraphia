using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScaler : MonoBehaviour {

    public Slider innerSlider;
    public Slider outerSlider;
    public RectTransform inner;
    public RectTransform outer;
    

    public void OnOuterSliderChange()
    {
        outer.sizeDelta = new Vector2(outerSlider.value, outerSlider.value);
    }
    public void OnInnerSliderChange()
    {
        inner.sizeDelta = new Vector2(innerSlider.value, innerSlider.value);
    }
}
