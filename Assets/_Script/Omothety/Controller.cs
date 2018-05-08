using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Punto
{
    A,B,C
}
public enum Speed
{
    NORMAL,SLOW,FAST,NONE
}

public class Controller : CallBackInterface {

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


    public int repetitions = 10;

    private Speed speed;

    private static float normalMeanTotalTime = 0;

    private int counter;
    private int Counter
    {
        get { return counter; }
        set
        {
            if (value == repetitions)
                UIManager.instance.ShowResult(GetResult());
            counter = value;
        }
    }


    private void Start()
    {
        startPoint = Punto.A;

    }

    public void SetSpeed(Speed s)
    {
        speed = s;
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

                Counter++;
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

    public override string GetResult()
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
        sum = 0;
        for (int i = 0; i < totalFigureTimes.Count; i++)
        {
            sum += totalFigureTimes[i];
        }
        if(speed != Speed.NORMAL)
            result += "mean total time: " + FormatFloat(sum / totalFigureTimes.Count) + " you had to do it in: ";
        switch (speed)
        {
            case Speed.SLOW: result += FormatFloat(normalMeanTotalTime * 1.5f); break;
            case Speed.FAST: result += FormatFloat(normalMeanTotalTime * .75f); break;
        }
        if (speed != Speed.NORMAL)
            result += " seconds";

        if (WasGood())
        {    
            result += "GOOD JOB!\n";
            if (speed == Speed.NORMAL)
                result+="now do it faster, do it in max " + FormatFloat(.75f * normalMeanTotalTime) + " seconds \n";
            else if(speed == Speed.FAST)
                result += "now do it slower, do it in min " + FormatFloat(1.5f * normalMeanTotalTime) + " seconds \n";
        }
        else
        {
            result += "!\n";
            if (speed == Speed.FAST)
                result += "you are not enough fast, do it in max " + FormatFloat(.75f * normalMeanTotalTime) + " seconds\n";
            else if (speed == Speed.SLOW)
                result += "you are not enough slow, do it in min " + FormatFloat(1.5f * normalMeanTotalTime) + " seconds \n";
        }


        // UIManager.instance.ShowResult(result);
        return result;
    }

    private string FormatFloat(float number)
    {
        return String.Format("{0:00.00}", number);
    }




    private string FormatNumber(float number)
    {
        return String.Format( "{0:00.00}", number*100);
    }

    public override bool WasGood()
    {
        float sum = 0;
        switch (speed)
        {
            case Speed.NORMAL:
                //calcola media tempi totali
                
                for (int i = 0; i < totalFigureTimes.Count; i++)
                {
                    sum += totalFigureTimes[i];
                }
                normalMeanTotalTime = sum/totalFigureTimes.Count;
                return true;
                break;
            case Speed.SLOW:
                
                for (int i = 0; i < totalFigureTimes.Count; i++)
                {
                    sum += totalFigureTimes[i];
                }
                if(sum / totalFigureTimes.Count >= 1.5f * normalMeanTotalTime)
                    return true;
                break;
            case Speed.FAST:

                for (int i = 0; i < totalFigureTimes.Count; i++)
                {
                    sum += totalFigureTimes[i];
                }
                if (sum / totalFigureTimes.Count <= .75f * normalMeanTotalTime)
                    return true;
                break;
        }
        
        return false;
    }
}
