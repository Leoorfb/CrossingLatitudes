using System.Collections.Generic;
using UnityEngine;

public class DealDamageGA : GameAction
{
    public int Amount = 1;

    public List<UnitView> Targets { get; set; }

    public DealDamageGA(int amount, List<UnitView> targets)
    {
        Targets = targets;
        Amount = amount;
    }
}
