using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Beat : MonoBehaviour
{

    public static Beat instance;

    public int target = 0;

    public float offset;
    public float tempo;
    [Range(0f, 1f)] public float errorPercentage = 0f;
    [HideInInspector] public bool inTime = false;
    public Swipe[] angles;
    public int sidesNumber;
    int counter = 0;
    public Text text;


    void Start()
    {
        StartCoroutine(Loop());
    }


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    public void ChangeTarget()
    {
        target++;
        if (target >= angles.Length)
            target = 0;
    }


    IEnumerator Loop()
    {
        float attesa = offset / 3;
        text.text = "3";
        yield return new WaitForSeconds(attesa);
        text.text = "2";
        yield return new WaitForSeconds(attesa);
        text.text = "1";
        yield return new WaitForSeconds(attesa - tempo * errorPercentage);
        text.text = "";
        inTime = true;
        yield return new WaitForSeconds(tempo * errorPercentage);

        angles[counter].StartSwiping(tempo);
        counter++;

        yield return new WaitForSeconds(tempo * errorPercentage);
        inTime = false;

        for (int i = 1; i < sidesNumber; i++)
        {

            yield return new WaitForSeconds(tempo - 2 * tempo * errorPercentage);
            inTime = true;
            yield return new WaitForSeconds(tempo * errorPercentage);
            angles[i].StartSwiping(tempo);
            yield return new WaitForSeconds(tempo * errorPercentage);
            inTime = false;
        }

        while (true)
        {
            yield return new WaitForSeconds(tempo - 2 * tempo * errorPercentage);
            inTime = true;
            yield return new WaitForSeconds(2 * tempo * errorPercentage);
            inTime = false;
        }
    }
}