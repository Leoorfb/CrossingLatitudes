using UnityEngine;

public class DrawCardEffect : EffectPlain
{
    public int amount = 1;


    public override string GetDescription()
    {
        return("comprou " + amount + " cartas");
    }

    public override void Perform()
    {
        Debug.Log(GetDescription());
    }
}
