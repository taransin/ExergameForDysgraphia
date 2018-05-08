using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : CallBackInterface {

    private Level level;
    private Color defaultColor;
    private int tapCounter = 0;
    public int[] counters;



    private void Start()
    {
        Init();
    }

    public void Init()
    {
        defaultColor = GetComponent<SpriteRenderer>().color;
        level = this.gameObject.GetComponentInParent<Level>();
        counters = new int[4];
    }


    public void Clicked()
    {
        if(level.phase != Phase.NOT_STARTED) {
            counters[(int)level.phase - 1]++;
            tapCounter++;

            if (level.inTime)
                StartCoroutine(ChangeColor(Color.green));
            else
                StartCoroutine(ChangeColor(Color.red));
        }
    }

    public override string GetResult()
    {
        string result = "";
        result += "TOO EARLY: " + counters[0] + "\n";
        result += "PERFECT:   " + counters[1] + "\n";
        result += "GOOD:      " + counters[2] + "\n";
        result += "TOO LATE:  " + counters[3] + "\n";
        if (WasGood())
            result += "Well Done!!" + "\n";
        else
            result += "Let's try an easier level!!" + "\n";
        return result;
    }


    private IEnumerator ChangeColor(Color color)
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
