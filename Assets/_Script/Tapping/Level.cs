using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum Phase
{
    NOT_STARTED, TOO_EARLY, EARLY, LATE, TOO_LATE
}


public class Level : MonoBehaviour {
    [HideInInspector]
    public bool inTime = false;
    [HideInInspector]
    public Phase phase = Phase.NOT_STARTED;

    public Song song;
    public Text counter;
    public GameObject nextLevelButton;
    public float errorPercentage = 0.3f;
    [SerializeField]
    public CallBackInterface resultObject;

    private void Awake()
    {
        if (song)
        {
            AudioSource _as = gameObject.AddComponent<AudioSource>();
            _as.clip = song.audio;
            _as.Play();
            StartCoroutine(Loop());
        }

        errorPercentage = UIManager.instance.GetTimeError();
    }

    // Use this for initialization
    void Start () {
        if(song)
            StartCoroutine(GameStartGui(song.offset));
    }
	

    private IEnumerator Loop()
    {
        float errore = 0;
        float time = Time.realtimeSinceStartup;

        float timeToWait = song.offset - song.tempo * errorPercentage;
        yield return new WaitForSeconds(timeToWait);
        phase = Phase.EARLY;
        errore = GetElapsedRealTime(time) - timeToWait;
        inTime = true;
        time = Time.realtimeSinceStartup;
        timeToWait = song.tempo * errorPercentage - errore;
        yield return new WaitForSeconds(timeToWait);
        phase = Phase.LATE;
        errore = GetElapsedRealTime(time) - timeToWait;

        time = Time.realtimeSinceStartup;
        timeToWait = song.tempo * errorPercentage - errore;
        yield return new WaitForSeconds(timeToWait);
        phase = Phase.TOO_LATE;
        errore = GetElapsedRealTime(time) - timeToWait;

        inTime = false;

        float tempo = 1;
        while (tempo <= song.beatCount - 3)
        {

            time = Time.realtimeSinceStartup;
            timeToWait = song.tempo/2 - song.tempo * errorPercentage - errore;
            yield return new WaitForSeconds(timeToWait);
            errore = GetElapsedRealTime(time) - timeToWait;
            phase = Phase.TOO_EARLY;

            time = Time.realtimeSinceStartup;
            timeToWait = song.tempo / 2 - song.tempo * errorPercentage - errore;
            yield return new WaitForSeconds(timeToWait);
            errore = GetElapsedRealTime(time) - timeToWait;
            phase = Phase.EARLY;
            inTime = true;


            time = Time.realtimeSinceStartup;
            timeToWait = song.tempo * errorPercentage - errore;
            yield return new WaitForSeconds(timeToWait);
            phase = Phase.LATE;
            errore = GetElapsedRealTime(time) - timeToWait;

            time = Time.realtimeSinceStartup;
            timeToWait = song.tempo * errorPercentage - errore;
            yield return new WaitForSeconds(timeToWait);
            phase = Phase.TOO_LATE;
            errore = GetElapsedRealTime(time) - timeToWait;

            inTime = false;




            if (tempo == song.stopAt)
                GetComponent<AudioSource>().Stop();


            tempo++;

        }
        phase = Phase.NOT_STARTED;

        if(resultObject != null)
            UIManager.instance.ShowResult(resultObject.GetResult());
        else
            UIManager.instance.ShowNextLevelButton();   



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
