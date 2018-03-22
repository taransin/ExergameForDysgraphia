using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour {

    public GameObject side;
    public GameObject swipe;
    private float tempo;
    bool swiping = false;
    private float elapsedTime = 0;
    GameObject instance;

    public GameObject firstArea;
    public GameObject secondArea;
	

	void Update () {
        if (swiping)
        {
            elapsedTime += Time.deltaTime;
            instance.transform.localScale = new Vector3(side.transform.localScale.x * elapsedTime / tempo,
                instance.transform.localScale.y,
                instance.transform.localScale.z);

            if (elapsedTime >= tempo)
                swiping = false;
        }
        else
            elapsedTime = 0;
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

    public bool ChangeArea()
    {
        if (this.gameObject.Equals(Beat.instance.angles[Beat.instance.target].gameObject))
        {
            if (!Beat.instance.inTime)
                StartCoroutine(ChangeColor(Color.red, Color.yellow, 0.5f));
            else
                StartCoroutine(ChangeColor(Color.green, Color.yellow, 0.5f));

            if (firstArea)
                firstArea.SetActive(false);
            if (secondArea)
                secondArea.SetActive(true);

            Beat.instance.ChangeTarget();
            return true;
        }
        return false;
    }


    IEnumerator ChangeColor(Color toChange, Color defaultColor, float time)
    {
        this.GetComponent<SpriteRenderer>().color = toChange;
        yield return new WaitForSeconds(time);
        this.GetComponent<SpriteRenderer>().color = defaultColor;
    }
}