using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour {
    [HideInInspector]
    public bool inTime = false;

    public Song song;
    public Text counter;
    public GameObject nextLevelButton;
    public float errorPercentage = 0.3f;

    private void Awake()
    {
        AudioSource _as = gameObject.AddComponent<AudioSource>();
        _as.clip = song.audio;
        _as.Play();

        StartCoroutine(Loop());
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(GameStartGui(song.offset));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Loop()
    {
        float errore = 0;
        float time = Time.realtimeSinceStartup;

        float timeToWait = song.offset - song.tempo * errorPercentage;
        yield return new WaitForSeconds(timeToWait);
        errore = GetElapsedRealTime(time) - timeToWait;
        inTime = true;
        time = Time.realtimeSinceStartup;
        timeToWait = 2 * song.tempo * errorPercentage - errore;
        yield return new WaitForSeconds(timeToWait);
        errore = GetElapsedRealTime(time) - timeToWait;
        inTime = false;
        float tempo = 1;
        while (tempo <= song.beatCount - 3)
        {
            time = Time.realtimeSinceStartup;
            timeToWait = song.tempo - 2 * song.tempo * errorPercentage - errore;
            yield return new WaitForSeconds(timeToWait);
            errore = GetElapsedRealTime(time) - timeToWait;
            inTime = true;
            time = Time.realtimeSinceStartup;
            timeToWait = 2 * song.tempo * errorPercentage - errore;
            yield return new WaitForSeconds(timeToWait);
            errore = GetElapsedRealTime(time) - timeToWait;
            inTime = false;

            if (tempo == song.stopAt)
                GetComponent<AudioSource>().Stop();

            Debug.Log(tempo + " <= " + song.beatCount);

            tempo++;

        }


        nextLevelButton.SetActive(true);
    }

    private float GetElapsedRealTime(float time)
    {
        return Time.realtimeSinceStartup - time;
    }


    private IEnumerator GameStartGui(float time)
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
