using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour {
    public int actualLevel = 0;
    public GameObject[] levels;
    public GameObject nextLevelPanel;
    public Text counter;

    public Speed omothetyLevel = Speed.NONE;

    public GameObject loadedLevel;

    private int[] randomLevelIsochrony;
    private int isochronyToPlay = 3;
    private int isochronyRepetitions = 0;
    private int lastPlayed = 3;


    public void ChangeLevel()
    {
        nextLevelPanel.SetActive(false);
        levels[actualLevel].SetActive(false);
        actualLevel++;
        levels[actualLevel].SetActive(true);
    }

    public void InstantiateNextLevel()
    {

        if (actualLevel == 2)
        {

            if (loadedLevel)
                Destroy(loadedLevel);
            ManageIsochronyLevels();
        }else if(actualLevel == 5)
        {
            switch (omothetyLevel)
            {
                case Speed.NONE:
                    omothetyLevel = Speed.NORMAL;
                    if (loadedLevel)
                        Destroy(loadedLevel);

                    break;

                case Speed.NORMAL:
                    omothetyLevel = Speed.FAST;
                    if (loadedLevel)
                        Destroy(loadedLevel);
                    break;
                case Speed.FAST:
                    if (loadedLevel.GetComponent<Level>().resultObject.WasGood())
                    {
                        omothetyLevel = Speed.SLOW;
                    }                 
                    if (loadedLevel)
                        Destroy(loadedLevel);

                    break;
                case Speed.SLOW:
                    if (loadedLevel.GetComponent<Level>().resultObject.WasGood())
                    {
                        actualLevel++;
                        UIManager.instance.ShowNextLevelButton();
                        return;
                    }
                    if (loadedLevel)
                        Destroy(loadedLevel);

                    break;
            }




            loadedLevel = Instantiate(levels[actualLevel]);
            Level level = loadedLevel.GetComponent<Level>();
            level.counter = counter;
            level.nextLevelButton = nextLevelPanel;
            level.resultObject.GetComponent<Controller>().SetSpeed(omothetyLevel);
            
        }
        else
        {
            if (loadedLevel)
                Destroy(loadedLevel);
            loadedLevel = Instantiate(levels[actualLevel]);
            actualLevel++;
            Level level = loadedLevel.GetComponent<Level>();
            level.counter = counter;
            level.nextLevelButton = nextLevelPanel;
        }


    }

    private void ManageIsochronyLevels()
    {
        if(isochronyToPlay == 3)
        {
            GenerateLevelsOrder();
            isochronyRepetitions++;
            isochronyToPlay = 0;
            //genera i livelli random
            //repetition ++
        }

        if (loadedLevel)
            Destroy(loadedLevel);
        loadedLevel = Instantiate(levels[2 + randomLevelIsochrony[isochronyToPlay]]);
        Level level = loadedLevel.GetComponent<Level>();
        level.counter = counter;
        level.nextLevelButton = nextLevelPanel;

        isochronyToPlay++;
        //carica e gioca al livello isochronyToPlay
        //isochronyToPlay++
        if(isochronyRepetitions==3 && isochronyToPlay == 3)
        {
            isochronyRepetitions = 0;
            actualLevel += 3;
        }

    }

    private void GenerateLevelsOrder()
    {

        randomLevelIsochrony = new int[3];
        for (int i = 0; i < 3; i++)
        {
            randomLevelIsochrony[i] = -1;
        }
        System.Random rnd = new System.Random();
        for(int i = 0; i < 3; i++)
        {
            int chosen;
            do
            {
                chosen = rnd.Next(0, 3);
            } while (Contains(randomLevelIsochrony, chosen));
            randomLevelIsochrony[i] = chosen;
        }


    }

    private bool Contains(int[] array, int chosen)
    {
        for(int i = 0; i < array.Length; i++)
        {
            if (array[i] == chosen) return true;
        }
        return false;
    }


    public void Reset()
    {
        //actualLevel = 0;
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
