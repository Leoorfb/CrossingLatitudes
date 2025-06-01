using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformEffectGA : GameAction
{
    public EffectPlain Effect { get; set; }

    public PerformEffectGA(EffectPlain effect)
    {
        Effect = effect;
    }
}
