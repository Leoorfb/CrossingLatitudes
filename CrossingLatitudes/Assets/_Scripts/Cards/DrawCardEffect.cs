using System.Collections;
using UnityEngine;

public class DrawCardEffect : EffectPlain
{
    public int amount = 1;


    public override string GetDescription()
    {
        return("Compra " + amount + " cartas. ");
    }

    public override IEnumerator Perform()
    {
        DrawCardsGA drawCardGA = new DrawCardsGA(amount);
        
        ActionSystem.Instance.Perform(drawCardGA);
        while (ActionSystem.Instance.IsPerforming)
        {
            yield return new WaitForEndOfFrame();
        }

        Debug.Log(GetDescription());
    }
}
