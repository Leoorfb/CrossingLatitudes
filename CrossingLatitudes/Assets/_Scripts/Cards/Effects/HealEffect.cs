using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : EffectPlain
{
    public int amount = 1;

    public override string GetDescription()
    {
        return ("Cura " + amount + " de PV. ");
    }

    public override GameAction GetGameAction()
    {
        Debug.Log("USOU CARTA DE CURAR");
        HealGA healGA = new(amount);
        return healGA;
    }
}
