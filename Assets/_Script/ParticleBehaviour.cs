using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour {

    void LateUpdate()
    {
        transform.LookAt(Beat.instance.swipes[Beat.instance.target].transform);

    }
}
