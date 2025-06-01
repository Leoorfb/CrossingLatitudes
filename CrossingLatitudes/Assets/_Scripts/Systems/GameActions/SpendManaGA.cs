using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpendManaGA : GameAction
{
    public int Amount = 1;

    public SpendManaGA(int amount)
    {
        Amount = amount;
    }
}
