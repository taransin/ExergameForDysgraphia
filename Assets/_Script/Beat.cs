using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour {


    public float offset;
    public float tempo;
    public float change;
    // Use this for initialization

    public Swipe[] swipes;

    int counter = 0;

	void Start () {
        StartCoroutine(Loop());
	}
	

    IEnumerator Loop()
    {
        yield return new WaitForSeconds(offset);
        while (counter < 3)
        {
            swipes[counter].StartSwiping(tempo);
            counter++;
            yield return new WaitForSeconds(tempo);
        }
    }
}
