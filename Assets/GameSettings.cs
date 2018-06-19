using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameSettings : MonoBehaviour {

    public Slider timeSlider;
    public float timeError;

    public Slider isochronyInnerSpaceSlider;
    public float isochronyInnerSpace;

    public Slider isochronyOuterSpaceSlider;
    public float isochronyOuterSpace;

    public Slider omothetyDeltaSlider;
    public float omothetyDelta;

    public Slider omothetyRoundsSlider;
    public int omothetyRounds;


    public void SaveSettings()
    {
        timeError = timeSlider.value;
        isochronyInnerSpace = (5.5f * isochronyInnerSpaceSlider.value) / 111;
        isochronyOuterSpace = (7.88f * isochronyOuterSpaceSlider.value) / 217;
        omothetyDelta = omothetyDeltaSlider.value / 50f;
        omothetyRounds = (int)omothetyRoundsSlider.value;

        SaveToFile.instance.AddConfiguration("timeDeltaError: " + timeSlider.value);
        SaveToFile.instance.AddConfiguration("isochronyInnerSpace: " + isochronyInnerSpaceSlider.value);
        SaveToFile.instance.AddConfiguration("isochronyOuterSpace: " + isochronyOuterSpaceSlider.value);
        SaveToFile.instance.AddConfiguration("omothetyDeltaSpace: " + omothetyDeltaSlider.value);
        SaveToFile.instance.AddConfiguration("omothetyRounds: " + (int)omothetyRoundsSlider.value);

    }


}
