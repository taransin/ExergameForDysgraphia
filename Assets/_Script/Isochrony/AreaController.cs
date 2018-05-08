using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour {

    public bool defaultValue;
    public bool triggered;

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        triggered = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && defaultValue)
        {
            triggered = true;
            collision.gameObject.GetComponent<Point>().ChangeColor(Color.black);
            Debug.Log("sono exter e tu sei uscito");
        }
        else
        {
            collision.gameObject.GetComponent<Point>().ChangeColor(Color.yellow);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !defaultValue)
        {
            triggered = true;
            collision.gameObject.GetComponent<Point>().ChangeColor(Color.black);
            Debug.Log("sono inter e tu sei entrato");
        }
        else
        {
            collision.gameObject.GetComponent<Point>().ChangeColor(Color.yellow);
        }
                       
    }




}
