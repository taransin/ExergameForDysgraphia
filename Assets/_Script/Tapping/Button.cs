using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour {

    private float time;
    private bool inTime = false;
    public Song song;
    public float errorPercentage = 0.3f;

    public GameObject nextLevel;

    public Text counter;

    private Color defaultColor;

    private void Awake()
    {
        AudioSource _as = gameObject.GetComponent<AudioSource>();
        _as.clip = song.audio;
        _as.Play();
        StartCoroutine(Loop());
        time = Time.realtimeSinceStartup;
    }

    private void Start()
    {
        defaultColor = GetComponent<SpriteRenderer>().color;
        StartCoroutine(GameStartGui(song.offset)); 
    }

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
        if (sp.color == defaultColor)
        {
            sp.color = color;
            yield return new WaitForSeconds(0.1f);
            sp.color = defaultColor;
        }

    }

    IEnumerator Loop() {

        float errore = 0;
        time = Time.realtimeSinceStartup;
        yield return new WaitForSeconds(song.offset - song.tempo * errorPercentage);
        //Debug.Log("volevo: " + (song.offset - song.tempo * errorPercentage) + " invece ho " + (Time.realtimeSinceStartup - time));
        errore = (Time.realtimeSinceStartup - time) - (song.offset - song.tempo * errorPercentage);
        inTime = true;
        time = Time.realtimeSinceStartup;
        yield return new WaitForSeconds(2*song.tempo * errorPercentage - errore);
        errore = (Time.realtimeSinceStartup - time) - (2 * song.tempo * errorPercentage - errore);
        //Debug.Log("volevo: " + (2 * song.tempo * errorPercentage) + " invece ho " + (Time.realtimeSinceStartup - time));
        inTime = false;
        float tempo = 1;
        while (tempo <= song.beatCount - 3)
        {
            time = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(song.tempo - 2 * song.tempo * errorPercentage - errore);
            errore = (Time.realtimeSinceStartup - time) - (song.tempo - 2 * song.tempo * errorPercentage - errore);
            //Debug.Log("volevo: " + (song.tempo - 2 * song.tempo * errorPercentage) + " invece ho " + (Time.realtimeSinceStartup - time));
            inTime = true;
            time = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(2*song.tempo * errorPercentage - errore);
            errore = (Time.realtimeSinceStartup - time) - (2 * song.tempo * errorPercentage - errore);
            //Debug.Log("volevo: " + (2 * song.tempo * errorPercentage) + " invece ho " + (Time.realtimeSinceStartup - time));
            inTime = false;

            if (tempo == song.stopAt)
                GetComponent<AudioSource>().Stop();

            Debug.Log(tempo + " <= " + song.beatCount);

            tempo++;

        }


        nextLevel.SetActive(true);
    }


    IEnumerator GameStartGui(float time)
    {
        float wait = time / 3;
        counter.text = "3";
        yield return new WaitForSeconds(wait);
        counter.text = "2";
        yield return new WaitForSeconds(wait);
        counter.text = "1";
        yield return new WaitForSeconds(wait);
        counter.text = "";
    }


}
