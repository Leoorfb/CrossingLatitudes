using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealGA : GameAction
{
    public int amount = 1;

    public HealGA(int amount)
    {
        this.amount = amount;
    }
}
