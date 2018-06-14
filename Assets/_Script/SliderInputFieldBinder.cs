using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class SliderInputFieldBinder : MonoBehaviour {

    public Slider slider;
    public InputField input;

    private float min;
    private float max;


    bool ignoreChange = false;


    private void Start()
    {
        min = slider.minValue;
        max = slider.maxValue;
        input.text = slider.value.ToString();
    }



    public void InputFieldChanged()
    {
        string text = input.text;
        float number;
        bool isNumber = float.TryParse(text, out number);
        if (isNumber)
        {
            if (number >= max)
            {
                input.text = max.ToString();
                slider.value = max;
            }
            else if (number <= min)
            {
                input.text = min.ToString();
                slider.value = min;
            }
            else
                slider.value = number;
        }
        else
        {
            if (text == "")
            {
                ignoreChange = true;
                slider.value = min;
            }
               
            else
                input.text = slider.value.ToString();
        }
        
    }
    public void SliderChanged()
    {
        if (!ignoreChange)
            input.text = slider.value.ToString();
        else
            ignoreChange = false;
    }



}
