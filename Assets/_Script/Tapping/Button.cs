using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    private Level level;
    private Color defaultColor;

    private void Start()
    {
        defaultColor = GetComponent<SpriteRenderer>().color;
        level = this.gameObject.GetComponentInParent<Level>();
        
    }

    public void Clicked()
    {
        if (level.inTime)
            StartCoroutine(ChangeColor(Color.green));
        else
            StartCoroutine(ChangeColor(Color.red));
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
}
