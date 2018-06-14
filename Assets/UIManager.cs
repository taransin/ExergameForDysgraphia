using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject initialMenuPanel;
    public GameObject nextLevelPanel;
    public GameObject resultPanel;
    public GameObject settingPanel;

    public GameSettings settings;


    public Text resultText;

    public LevelChanger _lc50;
    public LevelChanger _lc80;

    private LevelChanger _chosenLC;

    public static UIManager instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }



    public void ShowNextLevelButton()
    {
        if (_chosenLC)
        {
            if (_chosenLC.HaveFinished())
            {
                _chosenLC.Finish();
                nextLevelPanel.SetActive(false);
                initialMenuPanel.SetActive(true);
            }
            else
                nextLevelPanel.SetActive(true);     
        }
    }

    public float GetTimeError()
    {
        return settings.timeError;
    }

    public void Load50bpmGame()
    {
        _lc50.Reset();
        settingPanel.SetActive(true);
        //_lc50.InstantiateNextLevel();
        initialMenuPanel.SetActive(false);
        _chosenLC = _lc50;
    }

    public void Load80bpmGame()
    {
        _lc80.Reset();
        settingPanel.SetActive(true);
        //_lc80.InstantiateNextLevel();
        initialMenuPanel.SetActive(false);
        _chosenLC = _lc80;
    }

    public void StartGame()
    {
        //TODO: leggi tutte le stronzate
        //TODO: salvale su file
        settingPanel.SetActive(false);
        _chosenLC.InstantiateNextLevel();
    }


    public void NextLevel()
    {
        if (resultPanel.activeInHierarchy)
        {
            resultText.text = "";
            resultPanel.SetActive(false);
        }

        if (_chosenLC)
        {
            if (_chosenLC == _lc80 && _chosenLC.actualLevel == 1)
            {
                if (!_chosenLC.loadedLevel.GetComponent<Level>().resultObject.WasGood())
                {
                    _chosenLC.Finish();
                    Load50bpmGame();
                    nextLevelPanel.SetActive(false);
                    return;

                }    
            }


            if (_chosenLC.HaveFinished())
            {
                _chosenLC.Finish();
                initialMenuPanel.SetActive(true);
            }
            else  
                _chosenLC.InstantiateNextLevel();
        }
        nextLevelPanel.SetActive(false);
    }


    public void ShowResult(string result)
    {
        resultText.text = result;
        resultPanel.SetActive(true);
    }



}