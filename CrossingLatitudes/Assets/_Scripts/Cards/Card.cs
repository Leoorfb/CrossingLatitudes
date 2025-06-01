using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    public int cost {get; set;}

    public List<EffectPlain> effects => cardData.effects;

    private readonly CardData cardData;

    public Sprite sprite { get => cardData.sprite; }
    public string title { get => cardData.name; }

    public Card(CardData cardData)
    {
        this.cardData = cardData;
        cost = cardData.cost;
    }

    /*
    public IEnumerator PerformEffect()
    {
        Debug.Log(title + " effects:");

        foreach(var effect in effects)
        {
            yield return effect.Perform();
        }
    }
    */
}
