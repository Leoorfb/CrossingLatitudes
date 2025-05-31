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

        ShuffleDeck();
    }

    public Card DrawCard()
    {
        if(deckCards.Count == 0)
        {
            if (discardPileCards.Count == 0)
            {
                Debug.Log("acabou as cartas");
                return null;
            }

            ReturnDiscard();
        }

        Card c = deckCards[0];
        deckCards.RemoveAt(0);
        return c;
    }

    private static System.Random rng = new();
    public void ShuffleDeck()
    {

        int n = deckCards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = deckCards[k];
            deckCards[k] = deckCards[n];
            deckCards[n] = value;
        }
    }

    public void ReturnDiscard()
    {
        deckCards.AddRange(discardPileCards);
        discardPileCards.Clear();
        ShuffleDeck();
    }

    public void OnCardPlayed(Card card)
    {
        deckCards.Remove(card);
        discardPileCards.Add(card);
    }
}
