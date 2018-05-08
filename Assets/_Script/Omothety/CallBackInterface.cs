using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CallBackInterface: MonoBehaviour {
    public abstract string GetResult();
    public abstract bool WasGood();
}
