using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameSettings : MonoBehaviour {

    public Slider timeSlider;
    public float timeError;


    public void SaveSettings()
    {
        //TODO: to file

        timeError = timeSlider.value;
    }


}
