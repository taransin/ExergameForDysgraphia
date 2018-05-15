﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsochronyButton : Button
{

    public AreaController exter;
    public AreaController inter;

    private int stayedInsideInTime = 0;
    private int notStayedInsideInTime = 0;
    private int stayedInsideNotInTime = 0;
    private int notStayedInsideNotInTime = 0;

    private Level _level;
    int lap = 0;

    private void Start()
    {
        Init();
    }

    public new void Init()
    {
        base.Init();


        stayedInsideInTime = 0;
        notStayedInsideInTime = 0;
        stayedInsideNotInTime = 0;
        notStayedInsideNotInTime = 0;
        exter.Reset();
        inter.Reset();
        _level = this.gameObject.GetComponentInParent<Level>();
    }

    public override string GetResult()
    {
        string result = "";
        result += "IN TIME & STAYED INSIDE:   " + stayedInsideInTime + "\n";
        result += "IN TIME & NOT STAYED INSIDE:   " + notStayedInsideInTime + "\n";
        result += "NOT IN TIME & STAYED INSIDE:   " + stayedInsideNotInTime + "\n";
        result += "NOT IN TIME & NOT STAYED INSIDE:   " + notStayedInsideNotInTime + "\n";
        return result;
    }

    public override bool WasGood()
    {
        return base.WasGood();
    }

    public new void Clicked()
    {

        base.Clicked();
        if (!exter.triggered && !inter.triggered)
        {
            if (_level.inTime)
                stayedInsideInTime++;
            else
                stayedInsideNotInTime++;
        }
        else
        {
            if (_level.inTime)
                notStayedInsideInTime++;
            else
                notStayedInsideNotInTime++;
        }
        lap++;
        exter.Reset();
        inter.Reset();
    }
}