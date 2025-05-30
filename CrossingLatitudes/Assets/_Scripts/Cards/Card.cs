using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    public int cost {get; set;}
    public List<EffectPlain> effects { get; set;}

    private readonly CardData cardData;

    public Sprite sprite { get => cardData.sprite; }
    public string title { get => cardData.name; }

    public Card(CardData cardData)
    {
        this.cardData = cardData;
        effects = cardData.effects;
        cost = cardData.cost;
    }

    public void PerformEffect()
    {
        Debug.Log(title + " effects:");

        foreach(var effect in effects)
        {
            effect.Perform();
        }
    }
}
