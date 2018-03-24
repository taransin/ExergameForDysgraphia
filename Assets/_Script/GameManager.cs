using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    MAINMENU,
    INGAME
}

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Figure[] figures;

    public GameState gameState = GameState.MAINMENU;
    [Space]
    [Header("GUI")]
    public GameObject counterPanel;
    public Text counter;
    public GameObject mainMenu;
    public Slider time;
    public Slider Space;

    [Space]
    [Header("For testing")]
    [Range(0f, 1f)]
    public float timeErrorPercentage = 0f;
    public int accettableAreaSize = 5;
    public Figure runningGame;

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
            gameState = GameState.INGAME;
            SetupGui();
            runningGame.errorPercentage = timeErrorPercentage;
            runningGame.accettableAreaSize = accettableAreaSize;
            StartCoroutine(GameStartGui(runningGame.song.offset));
        }
        else
        {
            gameState = GameState.MAINMENU;
            SetupGui();
        }
            
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
        gameState = GameState.INGAME;
        SetupGui();
        runningGame = Instantiate(figure);
        runningGame.errorPercentage = time.value;
        runningGame.accettableAreaSize = (int) Space.value;
        StartCoroutine(GameStartGui(runningGame.song.offset));
        
    }

    void SetupGui()
    {
        switch (gameState)
        {
            case GameState.INGAME:
                mainMenu.SetActive(false);
                counterPanel.SetActive(true);
                break;
            case GameState.MAINMENU:
                mainMenu.SetActive(true);
                counterPanel.SetActive(false);
                break;

        }
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
