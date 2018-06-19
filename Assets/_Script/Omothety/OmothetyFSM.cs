using UnityEngine;


public enum OmothetyState
{
    INIT, SMALL_A, SMALL_B, BIG_A, BIG_C
}

public class OmothetyFSM {
    OmothetyState state;
    public bool changed;
    public OmothetyFSM() { state = OmothetyState.INIT; }

    public OmothetyState Action(string action)
    {
        changed = false;
        switch (action)
        {
            case "A":
                if (state == OmothetyState.INIT)
                {
                    changed = true;
                    state = OmothetyState.BIG_A;
                }

                if (state == OmothetyState.SMALL_B)
                {
                    changed = true;
                    state = OmothetyState.BIG_A;
                }

                if (state == OmothetyState.BIG_C)
                {
                    changed = true;
                    state = OmothetyState.SMALL_A;
                }

                break;
            case "B":
                if (state == OmothetyState.SMALL_A)
                {
                    changed = true;
                    state = OmothetyState.SMALL_B;
                }

                break;
            case "C":
                if (state == OmothetyState.BIG_A)
                {
                    changed = true;
                    state = OmothetyState.BIG_C;
                }

                break;
        }
        return state;
    }

    public OmothetyState GetNextState()
    {

        switch (state)
        {
            case OmothetyState.INIT:
                return OmothetyState.BIG_A;
            case OmothetyState.SMALL_A:
                return OmothetyState.SMALL_B;
            case OmothetyState.SMALL_B:
                return OmothetyState.BIG_A;
            case OmothetyState.BIG_A:
                return OmothetyState.BIG_C;
            case OmothetyState.BIG_C:
                return OmothetyState.SMALL_A;
            default:
                return OmothetyState.INIT;
        }
    }
}
