using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {


    private bool inTime = false;
    public Song song;
    public float errorPercentage = 0.3f;

    public void Clicked()
    {
        if (inTime)
            StartCoroutine(ChangeColor(Color.green));
        else
            StartCoroutine(ChangeColor(Color.red));
    }

    IEnumerator ChangeColor(Color color)
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        Color old = sp.color;
        sp.color = color;
        yield return new WaitForSeconds(1);
        sp.color = old;
    }

    IEnumerator Loop() { 

        yield return new WaitForSeconds(song.offset - song.tempo * errorPercentage);
        inTime = true;

        yield return new WaitForSeconds(2*song.tempo * errorPercentage);
        inTime = false;

        while (true)
        {
            yield return new WaitForSeconds(song.tempo - 2 * song.tempo * errorPercentage);
            inTime = true;
            yield return new WaitForSeconds(2*song.tempo * errorPercentage);
            inTime = false;
        }
    }


}
