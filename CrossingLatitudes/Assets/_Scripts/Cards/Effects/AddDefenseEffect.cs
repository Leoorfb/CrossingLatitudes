using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDefenseEffect : EffectPlain
{
    public int amount = 1;

    public override string GetDescription()
    {
        return ("Add" + amount + " to your defense. ");
    }

    public override GameAction GetGameAction()
    {
        Debug.Log("USOU CARTA DE Aumentar defesa");
        AddDefenseGA addDefenseGA = new(amount);
        return addDefenseGA;
    }
}
