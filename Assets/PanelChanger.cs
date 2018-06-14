using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelChanger : MonoBehaviour {


    public GameObject[] panels;

	// Use this for initialization
	void Start () {
        OpenPanel(0);
	}
	
    public void OpenPanel(int num)
    {
        foreach (GameObject o in panels)
            o.SetActive(false);
        panels[num].SetActive(true);
    }
}
