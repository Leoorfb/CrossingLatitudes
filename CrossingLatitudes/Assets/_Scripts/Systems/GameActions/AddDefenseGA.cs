using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDefenseGA : GameAction
{
    public int amount = 1;

    public AddDefenseGA(int amount)
    {
        this.amount = amount;
    }
}