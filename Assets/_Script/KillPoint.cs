using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPoint : MonoBehaviour
{

    public GameObject parSystem;
    private bool active = true;
    private bool inArea;


    public void KillPoints()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color c = sr.color;
        c.a = 0;
        sr.color = c;
        active = false;
    }

    public void KillParticles()
    {
        parSystem.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        
        if (active && collision.tag == "Angle")
            collision.GetComponent<Angle>().ChangeArea();

        if (active && collision.tag == "AccettableArea")
            parSystem.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "AccettableArea")
            inArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (active && collision.tag == "AccettableArea")
        {
            parSystem.SetActive(true);
            inArea = false;
        }
    }
}