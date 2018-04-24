using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public bool touched = false;
    public float timeTouched;
    private Controller controller;

    private void Start()
    {
        controller = gameObject.GetComponentInParent<Controller>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            touched = true;
            timeTouched = controller.Touched(this);
        }
            
    }

    public void Reset()
    {
        touched = false;
    }


}
