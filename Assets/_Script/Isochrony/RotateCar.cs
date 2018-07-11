using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class RotateCar : MonoBehaviour {


    public Vector3 center;

	
	// Update is called once per frame
	void Update () {

        Vector2 first = this.transform.position - center;
        float newX = 0;
        first = first.normalized;

        if(first.y > 0)
        {
            newX = -1;
        }
        else
        {
            newX = 1;
        }

        float newY = 0;

        if (newX == 0)
        {
            if (first.x > 0)
            {
                newY = 1;
            }
            else
            {
                newY = -1;
            }
        }
        else
        {
            newY = (first.x / first.y) * newX;
        }


        Vector2 direction = new Vector2(newX, newY);

        float angle = (Mathf.Atan(newX / newY) * Mathf.Rad2Deg + 90);
        if (first.x < 0)
            angle += 180;
        //Debug.Log("first: " + first  + "tan: " + (Mathf.Atan(newY / newX) * Mathf.Rad2Deg ));
        this.transform.rotation =  Quaternion.Euler(0,0, angle) ;

	}
}
