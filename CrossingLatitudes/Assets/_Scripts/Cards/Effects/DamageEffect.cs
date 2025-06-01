using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : EffectPlain
{
    public int damage = 1;

    public override string GetDescription()
    {
        return ("Deals " + damage + " damage. ");
    }

    public override GameAction GetGameAction()
    {
        DrawCardsGA drawCardGA = new(0);
        return drawCardGA;
    }
}
