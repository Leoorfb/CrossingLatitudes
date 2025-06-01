using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAttackEffect : EffectPlain
{
    public int amount = 1;

    public override string GetDescription()
    {
        return ("Adiciona " + amount + " de ataque. ");
    }

    public override GameAction GetGameAction()
    {
        Debug.Log("USOU CARTA DE Aumentar ataque");
        AddAttackGA addAttackGA = new(amount);
        return addAttackGA;
    }
}
