using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackGA : GameAction
{
    public UnitView Attacker { get; private set; }
    public UnitView Target { get; private set; }

    public UnitAttackGA(UnitView attacker, UnitView target)
    {
        Attacker = attacker;
        Target = target;
    }
}
