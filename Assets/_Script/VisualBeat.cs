using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualBeat : MonoBehaviour {

    public float tempo;
    public float multiplier = 2;

    private float elapsedTime = 0;
    private bool reducing;
    private Vector3 initialScale;
    private Vector3 beatScale;
	
    // Use this for initialization
	void Start () {
        initialScale = transform.localScale;
        beatScale = new Vector3(initialScale.x * multiplier, initialScale.y * multiplier, initialScale.z);
	}
	
	// Update is called once per frame
	void Update () {
        if (reducing)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(beatScale, initialScale, elapsedTime / tempo);
        }
	}

    public void Beat()
    {
        transform.localEulerAngles = beatScale;
        reducing = true;
        elapsedTime = 0;
    }

}
