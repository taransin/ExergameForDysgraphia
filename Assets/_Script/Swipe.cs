using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour {

    public GameObject side;

    public GameObject swipe;

    public float tempo;

    bool swiping = false;
    private float elapsedTime = 0;
    GameObject instance;
    Vector3 rotation;


    public GameObject firstArea;
    public GameObject secondArea;
	
	void Update () {
        if (swiping)
        {
            elapsedTime += Time.deltaTime;
            //Debug.Log("scale originale: " + swipe.transform.localScale.x + " attuale :" + swipe.transform.localScale.x * elapsedTime / tempo);
            instance.transform.localScale = new Vector3(side.transform.localScale.x * elapsedTime/tempo,
                instance.transform.localScale.y,
                instance.transform.localScale.z);

            if (elapsedTime >= tempo)
                swiping = false;
        }
	}

    public void StartSwiping(float tempo)
    {
        this.tempo = tempo;
        instance = Instantiate(swipe);
        instance.transform.rotation = side.transform.rotation;
        instance.transform.position = transform.position;
        swiping = true;

        instance.transform.localScale = new Vector3(side.transform.localScale.x * elapsedTime / tempo,
                                                    instance.transform.localScale.y,
                                                    instance.transform.localScale.z);
    }


    public void ChangeArea()
    {
        if(firstArea)
            firstArea.SetActive(false);
        if (secondArea)
            secondArea.SetActive(true);
    }

}
