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

public enum Circle
{
    SMALL, BIG
}

public class Controller : CallBackInterface {

    private static float meanTime;


    private float fastMultiplier = .75f;
    private float slowMultiplier = 1.5f;
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
    private Circle drawingCircle = Circle.BIG; 

    private static float normalMeanTotalTime = 0;

    private int counter;
    private int Counter
    {
        get { return counter; }
        set
        {
            if (value == repetitions)
            {
                string result = GetResult();
                SaveToFile.instance.AddResults("OMOTHETY - " + speed.ToString());
                SaveToFile.instance.AddResults(result);
                UIManager.instance.ShowResult(result);
            }
                
            counter = value;
        }
    }

    private Vector2 smallCenter;
    private Vector2 bigCenter;
    private float smallRadius;
    private float bigRadius;
    public float smallError;
    public float bigError;
    public CircleTouch touch;
    private bool exited = false;
    private bool finished = false;
    private OmothetyFSM fsm;


    private void Start()
    {
        slowMultiplier = UIManager.instance.GetSlowMultiplier();
        fastMultiplier = UIManager.instance.GetFastMultiplier();

        if (speed == Speed.FAST)
            timeFullRotation /= 2;
        else if (speed == Speed.SLOW)
            timeFullRotation *= 1.8f;
        startPoint = Punto.A;

        smallRadius = (list[(int)Punto.A].transform.position.x - list[(int)Punto.B].transform.position.x) / 2;

        smallCenter = new Vector2(
                list[(int)Punto.B].transform.position.x + smallRadius,
                list[(int)Punto.B].transform.position.y
            );

        bigRadius = (list[(int)Punto.A].transform.position.x - list[(int)Punto.C].transform.position.x) / 2;

        bigCenter = new Vector2(
            list[(int)Punto.C].transform.position.x + bigRadius,
            list[(int)Punto.C].transform.position.y
        );



        Debug.Log("small " + smallRadius + "big" + bigRadius + 
            "smallCenter" + smallCenter  + "big" + bigCenter);

        angle = initialAngle;


        smallError = UIManager.instance.GetOmothetyDelta();
        bigError = UIManager.instance.GetOmothetyDelta();

        repetitions = UIManager.instance.GetOmothetyRounds();

        fsm = new OmothetyFSM();
        list[(int)Punto.A].ChangeColor();
    }

    private bool dragonDrawing = true;
    private float angle;
    public float timeFullRotation = 2f;
    public float initialAngle;
    private bool dragonInTheSmall = false;
    public GameObject dragon;
    private int dragonRotationCount = 0;

    private bool firstTime = true;

    private int exitedSmallCircleCounter;
    private int exitedBigCircleCounter;


    private void Update()
    {
        if (finished) return;
        if (dragonDrawing)
        {
            angle += (Time.deltaTime / timeFullRotation) * 360;
            if (!dragonInTheSmall)
            {
                dragon.transform.position = (Vector3)bigCenter + (new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * bigRadius,
                Mathf.Sin(angle * Mathf.Deg2Rad) * bigRadius));
                if (angle - 360 * dragonRotationCount >= 360)                
                {
                    
                    dragonInTheSmall = true;
                }
            }
            else
            {
                dragon.transform.position = (Vector3)smallCenter + (new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * smallRadius,
                Mathf.Sin(angle * Mathf.Deg2Rad) * smallRadius));
                if (angle - 360 * dragonRotationCount >= 360 * 2)
                {
                    dragonRotationCount += 2;
                    dragonInTheSmall = false;
                }
            }
            
            if (angle >= 360 * 4 + initialAngle)
            {
                dragon.SetActive(false);//ciao
                dragonDrawing = false;
            }
        }


        if (!touch.ThereIsInput())
            return;
        GameObject inst = touch.GetInputInstance();
        Vector2 position = inst.transform.position;

        if(drawingCircle == Circle.SMALL)
        {
            float distance = (position - smallCenter).magnitude;

            if (!exited && (distance < smallRadius - smallError || distance > smallRadius + smallError))
            {
                SaveToFile.instance.AddLog("out internal circle");
                exited = true;
                exitedSmallCircleCounter++;
            }
        }
        else
        {
            float distance = (position - bigCenter).magnitude;

            if (!exited && (distance < bigRadius - bigError || distance > bigRadius + bigError))
            {
                SaveToFile.instance.AddLog("out external circle");
                exited = true;
                exitedBigCircleCounter++;
            }
        }

    }

    public void SetSpeed(Speed s)
    {
        speed = s;
    }

    public float Touched(Checkpoint c)
    {
        if (finished) return c.timeTouched;
        OmothetyState actualState = fsm.Action(c.name);
        if (fsm.changed)
        {

            SaveToFile.instance.AddLog("touched " + c.name + " - correct");
            if (c.name == "A")
                exited = false;
            if (actualState == OmothetyState.SMALL_A)
            {

                littleTime = Time.realtimeSinceStartup - list[(int)Punto.A].timeTouched;
                smallFigureTimes.Add(littleTime);
                
                foreach (Checkpoint checkpoint in list)
                    checkpoint.Reset();

                drawingCircle = Circle.SMALL;
            }
            else if(actualState == OmothetyState.BIG_A)
            {

                if (firstTime)
                {
                    ChangeCheckpointColor();
                    firstTime = false;
                    return Time.realtimeSinceStartup;

                }

                bigTime = Time.realtimeSinceStartup - list[(int)Punto.A].timeTouched;
                bigFigureTimes.Add(bigTime);

                foreach (Checkpoint checkpoint in list)
                    checkpoint.Reset();

                totalTime = littleTime + bigTime;
                totalFigureTimes.Add(totalTime);
                Counter++;
                if (counter == repetitions)
                    finished = true;
                drawingCircle = Circle.BIG;
            }
            ChangeCheckpointColor();

            return Time.realtimeSinceStartup;
        }
        SaveToFile.instance.AddLog("touched " + c.name + " - wrong");
        // dove stiamo andando?
        return c.timeTouched;
        
    }


    private void ChangeCheckpointColor()
    {
        foreach (Checkpoint c in list)
            c.ResetColor();
        OmothetyState nextState = fsm.GetNextState();
        if (nextState == OmothetyState.SMALL_A || nextState == OmothetyState.BIG_A)
            list[(int)Punto.A].ChangeColor();
        if (nextState == OmothetyState.SMALL_B)
            list[(int)Punto.B].ChangeColor();
        if (nextState == OmothetyState.BIG_C)
            list[(int)Punto.C].ChangeColor();
    }

    public override string GetResult()
    {
        string result = "";
        List<float> littleRelatives = new List<float>();
        List<float> bigRelatives = new List<float>();
        float sumSmall = 0;
        float sumBig = 0;
        //Calcolo tempi relativi
        for (int i = 0; i < totalFigureTimes.Count; i++)
        {
            littleRelatives.Add(smallFigureTimes[i] / totalFigureTimes[i]);
            sumSmall += smallFigureTimes[i] / totalFigureTimes[i];

            bigRelatives.Add(bigFigureTimes[i] / totalFigureTimes[i]);
            sumBig += bigFigureTimes[i] / totalFigureTimes[i];
        }
        //calcolo medie
        meanSmallTime = sumSmall / totalFigureTimes.Count;
        meanBigTime = sumBig / totalFigureTimes.Count;

        //calcolo deviazioni standard
        sumSmall = 0;
        foreach (float f in littleRelatives)
        {
            sumSmall =(f - meanSmallTime) * (f - meanSmallTime);
        }
        float deviazione;
        if (littleRelatives.Count - 1 == 0)
            deviazione = 0;
        else
            deviazione = Mathf.Sqrt(sumSmall / (littleRelatives.Count-1));
 
        result += "media cerchio piccolo: " + FormatNumber(meanSmallTime) + "%\ndeviazione cerchio piccolo: " + FormatNumber(deviazione) +"%\n";

        sumBig = 0;
        foreach (float f in bigRelatives)
        {
            sumBig = (f - meanBigTime) * (f - meanBigTime);
        }
        if(bigRelatives.Count - 1 == 0)
            deviazione = 0;
        else
            deviazione = Mathf.Sqrt(sumBig / (bigRelatives.Count - 1));
        result += "media cerchio grande: " + FormatNumber(meanBigTime) + "%\ndeviazione cerchio grande: " + FormatNumber(deviazione) + "%\n";

        //calcolo prossimo tempo per giro totale
        float sum = 0;
        for (int i = 0; i < totalFigureTimes.Count; i++)
        {
            sum += totalFigureTimes[i];
        }

        if (speed == Speed.NORMAL)
        {
            result += "mean total time: " + FormatFloat(sum / totalFigureTimes.Count) + " seconds";
            meanTime = sum / totalFigureTimes.Count;
        }

        if (speed != Speed.NORMAL)
            result += "mean total time: " + FormatFloat(sum / totalFigureTimes.Count) + " you had to do it in: ";
        switch (speed)
        {
            case Speed.SLOW: result += FormatFloat(meanTime * slowMultiplier); break;
            case Speed.FAST: result += FormatFloat(meanTime * fastMultiplier); break;
        }
        if (speed != Speed.NORMAL)
            result += " seconds";
        result += "\n went out small " + exitedSmallCircleCounter + "times, big: " 
            + exitedBigCircleCounter + " times\n";
        if (WasGood())
        {    
            result += "GOOD JOB!\n";
            if (speed == Speed.NORMAL)
                result+="now do it faster, do it in max " + FormatFloat(fastMultiplier * normalMeanTotalTime) + " seconds \n";
            else if(speed == Speed.FAST)
                result += "now do it slower, do it in min " + FormatFloat(slowMultiplier * normalMeanTotalTime) + " seconds \n";
        }
        else
        {
            result += "ALMOST THERE!\n";
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
                //calcola media tempi total      
                for (int i = 0; i < totalFigureTimes.Count; i++)
                {
                    sum += totalFigureTimes[i];
                }
                normalMeanTotalTime = sum/totalFigureTimes.Count;
                return true;
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