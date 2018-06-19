using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public bool touched = false;
    public float timeTouched;
    private Controller controller;
    private SpriteRenderer _sr;
    private Color defaultColor;
    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        defaultColor = _sr.color;
    }
    private void Start()
    {
        controller = gameObject.GetComponentInParent<Controller>();
        timeTouched = Time.realtimeSinceStartup;
       
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

    public void ResetColor()
    {
        _sr.color = defaultColor;
    }

    public void ChangeColor()
    {
        _sr.color = Color.yellow;
    }




}
