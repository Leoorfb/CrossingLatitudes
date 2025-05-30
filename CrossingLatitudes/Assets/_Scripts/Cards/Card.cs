using UnityEngine;

public class Card
{
    public int cost {get; set;}
    public string effect { get; set;}

    private readonly CardData cardData;

    public Sprite sprite { get => cardData.sprite; }
    public string title { get => cardData.name; }

    public Card(CardData cardData)
    {
        this.cardData = cardData;
        effect = cardData.effect;
        cost = cardData.cost;
    }

    public void PerformEffect()
    {
        Debug.Log(effect + " performed - cost of " + cost);
    }
}
