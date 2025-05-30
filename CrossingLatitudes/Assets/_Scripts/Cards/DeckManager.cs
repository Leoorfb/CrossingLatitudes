using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    private static DeckManager _instance;

    public static DeckManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DeckManager>();
            }

            return _instance;
        }
    }


    [SerializeField] private List<CardData> initialDeckCardsData;

    private List<Card> deckCards;
    private List<Card> discardPileCards;

    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        deckCards = new();
        discardPileCards = new();

        foreach(var cardData in initialDeckCardsData)
        {
            Card card = new(cardData);
            deckCards.Add(card);
        }

    }

    public Card DrawCard()
    {
        Card c = deckCards[0];
        deckCards.RemoveAt(0);
        discardPileCards.Add(c);
        return c;
    }

    public void ShuffleDeck()
    {
        Debug.Log("Embaralhar não implementado");
    }

    public void ReturnDiscard()
    {
        deckCards.AddRange(discardPileCards);
        discardPileCards.Clear();
    }

}
