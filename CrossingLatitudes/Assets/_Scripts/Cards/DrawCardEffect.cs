using System.Collections;
using UnityEngine;

public class DrawCardEffect : EffectPlain
{
    public int amount = 1;


    public override string GetDescription()
    {
        return("Compra " + amount + " cartas. ");
    }

    public override GameAction GetGameAction()
    {
        Debug.Log("USOU CARTA DE COMPRAR");
        DrawCardsGA drawCardGA = new(amount);
        return drawCardGA;
    }
}
