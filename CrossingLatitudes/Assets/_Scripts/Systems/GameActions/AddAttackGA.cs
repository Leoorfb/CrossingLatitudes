using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAttackGA : GameAction
{
    public int amount = 1;

    public AddAttackGA(int amount)
    {
        this.amount = amount;
    }
}
