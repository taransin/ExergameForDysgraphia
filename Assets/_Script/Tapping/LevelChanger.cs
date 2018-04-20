using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour {
    public int actualLevel = 0;
    public GameObject[] levels;
    public GameObject nextLevelPanel;
    public Text counter;

    private GameObject loadedLevel;

    public void ChangeLevel()
    {
        nextLevelPanel.SetActive(false);
        levels[actualLevel].SetActive(false);
        actualLevel++;
        levels[actualLevel].SetActive(true);
    }

    public void InstantiateNextLevel()
    {

        if (!loadedLevel)
        {
            loadedLevel = Instantiate(levels[actualLevel]);
            actualLevel++;
            Level level = loadedLevel.GetComponent<Level>();
            level.counter = counter;
            level.nextLevelButton = nextLevelPanel;
        }
        else
        {
            Destroy(loadedLevel);
            loadedLevel = Instantiate(levels[actualLevel]);
            actualLevel++;
            Level level = loadedLevel.GetComponent<Level>();
            level.counter = counter;
            level.nextLevelButton = nextLevelPanel;

        }
    }

    public void Reset()
    {
       // actualLevel = 0;
        if (loadedLevel)
            Destroy(loadedLevel);
    }


    public bool HaveFinished()
    {
        return actualLevel == levels.Length;
    }

    public void Finish()
    {
        if (loadedLevel)
            Destroy(loadedLevel);

    }



}
