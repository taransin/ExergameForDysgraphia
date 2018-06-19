using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : CallBackInterface
{

    private Level level;
    private Color defaultColor;
    private int tapCounter = 0;
    public int[] counters;
    private SpriteRenderer _sr;

    private string[] names = { "TOO EARLY", "PERFECT", "GOOD", "TOO LATE" };


    private void Start()
    {
        Init();
        if(!gameObject.name.Contains("Circle"))
            SaveToFile.instance.AddLog(gameObject.name);
    }

    public void Init()
    {
        _sr = GetComponent<SpriteRenderer>();
        if (_sr)
            defaultColor = GetComponent<SpriteRenderer>().color;
        level = this.gameObject.GetComponentInParent<Level>();
        counters = new int[4];
    }


    public virtual void Clicked()
    {
        if (level.phase != Phase.NOT_STARTED)
        {
            counters[(int)level.phase - 1]++;
            SaveToFile.instance.AddLog("clicked - " + names[(int)level.phase - 1]);
            tapCounter++;
            if (_sr)
            {
                if (level.inTime)
                    StartCoroutine(ChangeColor(Color.green));
                else
                    StartCoroutine(ChangeColor(Color.red));
            }
        }
    }

    public override string GetResult()
    {
        string result = "";
        result += names[0] + ": " + counters[0] + "\n";
        result += names[1] + ":   " + counters[1] + "\n";
        result += names[2] + ":      " + counters[2] + "\n";
        result += names[3] + ":  " + counters[3] + "\n";
        if (WasGood())
            result += "Well Done!!" + "\n";
        else
            result += "Let's try an easier level!!" + "\n";
        return result;
    }


    public IEnumerator ChangeColor(Color color)
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        if (sp.color == defaultColor)
        {
            sp.color = color;
            yield return new WaitForSeconds(0.1f);
            sp.color = defaultColor;
        }

    }

    public override bool WasGood()
    {
        Debug.Log("test: " + (tapCounter * 0.3f > (counters[(int)Phase.TOO_EARLY - 1] + counters[(int)Phase.TOO_LATE - 1])));
        if (tapCounter * 0.3f > (counters[(int)Phase.TOO_EARLY - 1] + counters[(int)Phase.TOO_LATE - 1]))
            return true;
        return false;
    }
}
