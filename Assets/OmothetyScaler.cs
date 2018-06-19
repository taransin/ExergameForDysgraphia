using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OmothetyScaler : MonoBehaviour {

    public RectTransform outerOuter;
    public RectTransform outerInner;
    public RectTransform innerOuter;
    public RectTransform innerInner;

    public Slider slider;

    private void Start()
    {
        outerOuter.sizeDelta = new Vector2(25 + 200, 25 + 200);
        outerInner.sizeDelta = new Vector2(200 - 25, 200 - 25);

        innerOuter.sizeDelta = new Vector2(25 + 85, 25 + 85);
        innerInner.sizeDelta = new Vector2(85 - 25, 85 - 25);
    }

    public void OnSliderValueChange()
    {
        outerOuter.sizeDelta = new Vector2(slider.value + 200, slider.value + 200);
        outerInner.sizeDelta = new Vector2(200 - slider.value, 200 - slider.value);

        innerOuter.sizeDelta = new Vector2(slider.value + 85, slider.value + 85);
        innerInner.sizeDelta = new Vector2(85 - slider.value, 85 - slider.value);
    }


}
