using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPoint : MonoBehaviour
{

    public GameObject parSystem;
    
    private bool active = true;


    private bool inArea = false;

    private void Update()
    {
        if (active && !inArea)
            parSystem.SetActive(true);
        else
            parSystem.SetActive(false);
    }


    public void Kill()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color c = sr.color;
        c.a = 0;
        sr.color = c;
        active = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (active && collision.tag == "AccettableArea")
        {
            parSystem.SetActive(true);
            inArea = false;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "AccettableArea")
        {
            inArea = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active && collision.name.Contains("Angle"))
        {
            collision.GetComponent<Swipe>().ChangeArea();
        }

        if (active && collision.tag == "AccettableArea")
        {
            parSystem.SetActive(false);
        }
    }



    public void KillParticles()
    {
        parSystem.SetActive(false);
    }
}
