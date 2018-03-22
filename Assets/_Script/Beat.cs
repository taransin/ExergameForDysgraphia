using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Beat : MonoBehaviour
{

    [Range(0f, 1f)]
    public float errorPercentage = 0f;

    public float offset;
    public float tempo;
    public float change;

    //[HideInInspector]
    public bool inTime = false;

    public Swipe[] swipes;

    int counter = 0;


    public int sidesNumber;

    public static Beat instance;

    public int target = 0;


    public Text text;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }


    void Start()
    {
        StartCoroutine(Loop());
    }


    public void ChangeTarget()
    {
        target++;
        if (target >= swipes.Length)
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

        swipes[counter].StartSwiping(tempo);
        counter++;

        yield return new WaitForSeconds(tempo * errorPercentage);
        inTime = false;


        for (int i = 1; i < sidesNumber; i++)
        {

            yield return new WaitForSeconds(tempo - 2 * tempo * errorPercentage);
            inTime = true;
            yield return new WaitForSeconds(tempo * errorPercentage);
            swipes[i].StartSwiping(tempo);
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
