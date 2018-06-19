using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public enum SliderType
{
    INNER, OUTER, INTEGER, DECIMAL
}

public class SettingSliderTextBinder : MonoBehaviour {

    public Slider slider;
    public Text text;
    public SliderType type;
    private float min;
    private float max;


    bool ignoreChange = false;


    private void Start()
    {
        min = slider.minValue;
        max = slider.maxValue;
        if (type == SliderType.INNER)
            OnInnerSliderChanged();
        else if (type == SliderType.OUTER)
            OnOuterSliderChanged();
        else if (type == SliderType.INTEGER)
            OnSliderChangeInteger();
        else
            OnSliderChangeDecimal();
    }



    public void OnInnerSliderChanged()
    {
        text.text = (130 - slider.value).ToString();
    }

    public void OnOuterSliderChanged()
    {
        text.text = (slider.value - 200).ToString();
    }

    public void OnSliderChangeDecimal()
    {
        
        text.text = slider.value.ToString("0.00");
    }

    public void OnSliderChangeInteger()
    {

        text.text = slider.value.ToString();
    }

    public void OnSliderChangePercentage()
    {
        text.text = slider.value.ToString()+"%";
    }

}
