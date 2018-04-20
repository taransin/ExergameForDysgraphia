using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject initialMenuPanel;
    public GameObject nextLevelPanel;
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


    public void Load50bpmGame()
    {
        _lc50.Reset();
        _lc50.InstantiateNextLevel();
        initialMenuPanel.SetActive(false);
        _chosenLC = _lc50;
    }

    public void Load80bpmGame()
    {
        _lc80.Reset();
        _lc80.InstantiateNextLevel();
        initialMenuPanel.SetActive(false);
        _chosenLC = _lc80;
    }

    public void NextLevel()
    {
        if (_chosenLC)
        {
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


}