using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Punto
{
    A,B,C
}


public class Controller : MonoBehaviour, CallBackInterface {

    public Checkpoint[] list;
    private Punto startPoint;

    private Checkpoint reached;

    private List<float> smallFigureTimes = new List<float>();
    private List<float> bigFigureTimes = new List<float>();
    private List<float> totalFigureTimes = new List<float>();

    private float meanSmallTime;
    private float meanBigTime;

    private float anotherVariableBecauseThereAreNotEnoughVariablesForUsToUse, Thanks;

    private float littleTime;
    private float bigTime;
    private float totalTime;


    private float littleRelTime;
    private float bigRelTime;
    private Level level;

    public int repetitions = 10;

    private int counter = 0;


    private void Start()
    {
        level = GetComponentInParent<Level>();
        startPoint = Punto.A;
    }

    private void Update()
    {
        if (counter >= repetitions)
        {
            Callback();
            counter = 0;
        }
    }

    public float Touched(Checkpoint c)
    {
        if (c == list[(int)Punto.A])
        {
            if (list[(int)Punto.C].touched)
            {
                bigTime = Time.realtimeSinceStartup - list[(int)Punto.A].timeTouched;

                bigFigureTimes.Add(bigTime);

                foreach (Checkpoint checkpoint in list)
                    checkpoint.Reset();
                totalTime = littleTime + bigTime;
                totalFigureTimes.Add(totalTime);

                counter++;
            }
            else if (list[(int)Punto.B].touched)
            {
                littleTime = Time.realtimeSinceStartup - list[(int)Punto.A].timeTouched;
                smallFigureTimes.Add(littleTime);
                foreach (Checkpoint checkpoint in list)
                    checkpoint.Reset();
            }
        }


        return Time.realtimeSinceStartup;
    }

    public void Callback()
    {
        string result = "";
        List<float> littleRelatives = new List<float>();

        int lenght = smallFigureTimes.Count < totalFigureTimes.Count ? smallFigureTimes.Count  : totalFigureTimes.Count;
        float sum = 0;
        for (int i = 0; i < lenght; i++)
        {
            littleRelatives.Add(smallFigureTimes[i] / totalFigureTimes[i]);
            sum += smallFigureTimes[i] / totalFigureTimes[i];
        }

        meanSmallTime = sum / lenght;

        List<float> bigRelatives = new List<float>();
        lenght = bigFigureTimes.Count < totalFigureTimes.Count ? bigFigureTimes.Count : totalFigureTimes.Count;
        sum = 0;
        for (int i = 0; i < lenght; i++)
        {
            bigRelatives.Add(bigFigureTimes[i] / totalFigureTimes[i]);
            sum += bigFigureTimes[i] / totalFigureTimes[i];
        }

        meanBigTime = sum / lenght;

        littleRelatives.ForEach(e => Debug.Log("small: " + e));
        Debug.Log("mean small : " + meanSmallTime);
        sum = 0;
        foreach (float f in littleRelatives)
        {
            sum =(f - meanSmallTime) * (f - meanSmallTime);
        }
        float deviazione = Mathf.Sqrt(sum / (littleRelatives.Count));
        Debug.Log("deviazione standard small : " + deviazione );

        result += "media cerchio piccolo: " + FormatNumber(meanSmallTime) + "%\ndeviazione cerchio piccolo: " + FormatNumber(deviazione) +"%\n";

        Debug.Log("--------------------------------------");
        bigRelatives.ForEach(e => Debug.Log("big: " + e));
        Debug.Log("mean big : " + meanBigTime);
        sum = 0;
        foreach (float f in bigRelatives)
        {
            sum = (f - meanBigTime) * (f - meanBigTime);
        }
        deviazione = Mathf.Sqrt(sum / (bigRelatives.Count - 1));
        Debug.Log("deviazione standard big : " + deviazione);
        result += "media cerchio grande: " + FormatNumber(meanBigTime) + "%\ndeviazione cerchio grande: " + FormatNumber(deviazione) + "%\n";
        UIManager.instance.ShowResult(result);
    }

    private string FormatNumber(float number)
    {
        return String.Format( "{0:00.00}", number*100);
    }

}
