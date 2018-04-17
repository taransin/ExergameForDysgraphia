using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour {
    public int actualLevel = 0;
    public GameObject[] levels;
    public GameObject canvas;

    public void ChangeLevel()
    {
        canvas.SetActive(false);
        levels[actualLevel].SetActive(false);
        actualLevel++;
        levels[actualLevel].SetActive(true);
    }

}
