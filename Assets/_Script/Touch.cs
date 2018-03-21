using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour {

    public GameObject point;

    private GameObject instance;

    Plane objPlane;
	// Use this for initialization
	void Start () {
        objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            float distance;
            if(objPlane.Raycast(ray, out distance)){
                instance = Instantiate(point, ray.GetPoint(distance), Quaternion.identity);
            }
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            float distance;
            if (objPlane.Raycast(ray, out distance))
            {
                instance.transform.position = ray.GetPoint(distance);
            }
        }

        if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled))
        {

            KillPoint kp = instance.GetComponent<KillPoint>();
            kp.Kill();
            kp.KillParticles();
            
        }
    }
}
