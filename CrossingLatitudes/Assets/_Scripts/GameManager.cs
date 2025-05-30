using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<CardData> cardDatas;
    [SerializeField] private CardView cardView;

    private List<Card> deck;
    [SerializeField] private int startDeckSize = 10;

    private void Start()
    {
        deck = new();
        for (int i = 0; i < startDeckSize; i++)
        {
            CardData data = cardDatas[Random.Range(0, cardDatas.Count)];
            Card card = new(data);
            deck.Add(card);
        }
    }

    public void DrawCard()
    {
        Card drawnCard = deck[Random.Range(0, deck.Count)];
        deck.Remove(drawnCard);
        CardView view = Instantiate(cardView);
        view.Setup(drawnCard);
    }
    
}
