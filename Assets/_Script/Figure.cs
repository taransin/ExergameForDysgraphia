using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Figure : MonoBehaviour
{

    private int target = 0;

    public Song song;
    [HideInInspector] public float errorPercentage = 0f;
    [HideInInspector] public int accettableAreaSize = 1;

    public GameObject visualBeatObject;
    private VisualBeat visualBeatInstance;

    public bool inTime = false;

    // figure data
    public Angle[] angles;
    public int sidesNumber;
    public GameObject[] accettableAreas;

    private void Awake()
    {
        GetComponent<AudioSource>().clip = song.audio;
    }

    void Start()
    {
        foreach(GameObject g in accettableAreas)
        {
            Vector3 scale = g.transform.localScale;
            g.transform.localScale = new Vector3(scale.x, accettableAreaSize, scale.z);
        }
            
        StartCoroutine(Loop());
    }

    public void ChangeTarget()
    {
        target++;
        if (target >= angles.Length)
            target = 0;
    }

    public GameObject GetNextTarget()
    {
        return angles[target].gameObject;
    }

    IEnumerator Loop()
    {
        visualBeatInstance = Instantiate(visualBeatObject, new Vector3(), Quaternion.identity).GetComponent<VisualBeat>();
        if (visualBeatInstance)
            visualBeatInstance.tempo = song.tempo;

        yield return new WaitForSeconds(song.offset - song.tempo * errorPercentage);
        inTime = true;
        // yield return new WaitForSeconds(song.tempo * errorPercentage);
                
        angles[0].StartSwiping(song.tempo);
        if (visualBeatInstance)
            visualBeatInstance.Beat();
        yield return new WaitForSeconds(song.tempo * errorPercentage);
        inTime = false;

        for (int i = 1; i < sidesNumber; i++)
        {

            yield return new WaitForSeconds(song.tempo - 2 * song.tempo * errorPercentage);
            inTime = true;
            yield return new WaitForSeconds(song.tempo * errorPercentage);
            angles[i].StartSwiping(song.tempo);
            if (visualBeatInstance)
                visualBeatInstance.Beat();
            yield return new WaitForSeconds(song.tempo * errorPercentage);
            inTime = false;
        }

        while (true)
        {
            yield return new WaitForSeconds(song.tempo - 2 * song.tempo * errorPercentage);
            inTime = true;
            yield return new WaitForSeconds(song.tempo * errorPercentage);
            if (visualBeatInstance)
                visualBeatInstance.Beat();
            yield return new WaitForSeconds(song.tempo * errorPercentage);
            inTime = false;
        }
    }
}