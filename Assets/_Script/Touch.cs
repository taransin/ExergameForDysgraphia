using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{

    public GameObject point;
    private GameObject instance;
    Plane objPlane;

    public bool useMouse = false;


    void Start()
    {
        useMouse = UIManager.instance.useMouse;
        objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);
        Input.multiTouchEnabled = false;
    }

    void Update()
    {
        if (GameManager.instance.gameState == GameState.INGAME && ThereIsInput())
        {
            if (InputStarted())
            {
                Ray ray = Camera.main.ScreenPointToRay(GetInputPosition());
                float distance;
                if (objPlane.Raycast(ray, out distance))
                    instance = Instantiate(point, ray.GetPoint(distance), Quaternion.identity);
            }
            else if (InputOngoing())
            {
                Ray ray = Camera.main.ScreenPointToRay(GetInputPosition());
                float distance;
                if (objPlane.Raycast(ray, out distance))
                    instance.transform.position = ray.GetPoint(distance);
            }
            else if (InputEnded() && instance)
            {
                KillPoint kp = instance.GetComponent<KillPoint>();
                kp.KillPoints();
                kp.KillParticles();

            }
        }
    }


    private bool ThereIsInput()
    {
        if (useMouse)
            return Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0);
        else
            return Input.touchCount > 0;
    }

    private Vector3 GetInputPosition()
    {
        if (useMouse)
            return Input.mousePosition;
        else
            return (Input.GetTouch(0).position);
    }
    private bool InputStarted()
    {
        if (useMouse)
            return Input.GetMouseButtonDown(0);
        else
            return Input.GetTouch(0).phase == TouchPhase.Began;
    }

    private bool InputOngoing()
    {
        if (useMouse)
            return Input.GetMouseButton(0);
        else
            return Input.GetTouch(0).phase == TouchPhase.Moved;
    }

    private bool InputEnded()
    {
        if (useMouse)
            return Input.GetMouseButtonUp(0);
        else
            return (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled);
    }
}