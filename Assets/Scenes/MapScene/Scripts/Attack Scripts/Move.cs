using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public MoveBase Base {get; set;}
    public int PP{get; set;}

    public Move(MoveBase iBase)
    {
        Base = iBase;
        PP = iBase.PP;
    }
}
