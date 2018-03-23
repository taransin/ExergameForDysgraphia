using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    [Range(0f, 1f)] public float timeErrorPercentage = 0f;
    public int accettableAreaSize = 5;

    public Figure[] figures;

    public Figure runningGame;

    public Text text;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


	// Use this for initialization
	void Start () {
        if (runningGame) //for testing
        {
            runningGame.errorPercentage = timeErrorPercentage;
            StartCoroutine(GameStartGui(runningGame.song.offset));
        }
            
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Figure GetFigureByName(string name)
    {
        foreach (Figure f in figures)
            if (f.name == name)
                return f;
        return null;
    }
    public void StartLevel(string name)
    {
        Figure figure = GetFigureByName(name);
        if (!figure)
        {
            Debug.Log("figura " + name + " non trovata!");
            return;
        }

        runningGame = Instantiate(figure);
        runningGame.errorPercentage = timeErrorPercentage;
        StartCoroutine(GameStartGui(runningGame.song.offset));
        
    }


    IEnumerator GameStartGui(float time)
    {
        float wait = time / 3;
        text.text = "3";
        yield return new WaitForSeconds(wait);
        text.text = "2";
        yield return new WaitForSeconds(wait);
        text.text = "1";
        yield return new WaitForSeconds(wait);
        text.text = "";
    }

}
