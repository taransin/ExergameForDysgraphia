using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour {

    void LateUpdate()
    {
        transform.LookAt(GameManager.instance.runningGame.GetNextTarget().transform);
    }
}