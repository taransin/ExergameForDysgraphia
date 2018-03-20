using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPoint : MonoBehaviour
{
    public static int counter = 1;

    public GameObject area;

    public void Kill()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color c = sr.color;
        c.a = 0;
        sr.color = c;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "AccettableArea")
        {
            counter++;
            Debug.Log("fottiti merda, disgrafico del cazzo! hai sbagliato " + counter + " volte, cazzo!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Angle"))
        {
            counter--;
            collision.GetComponent<Swipe>().ChangeArea();
        }
    }

}
