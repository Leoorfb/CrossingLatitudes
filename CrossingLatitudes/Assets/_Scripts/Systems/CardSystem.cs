using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{
    [SerializeField] public int maxHandSize;
    [SerializeField] private HandManager handManager;
    private readonly List<Card> drawPile = new();
    private readonly List<Card> discardPile = new();
    private readonly List<Card> handPile = new();

    [SerializeField] private List<CardData> initialDeckCardsData;

    private void Start()
    {
        foreach (var cardData in initialDeckCardsData)
        {
            Card card = new(cardData);
            drawPile.Add(card);
        }

        ShuffleDeck();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ActionSystem.Instance.IsPerforming) return;

            DrawCardsGA drawCardGA = new(1);
            ActionSystem.Instance.Perform(drawCardGA);
        }
    }

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<DrawCardsGA>(DrawCardPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardsGA>();
    }

    // Performers

    private IEnumerator DrawCardPerformer(DrawCardsGA drawCardsGA)
    {
        for(int i = 0; i < drawCardsGA.amount; i++)
        {
            if (handPile.Count >= maxHandSize)
            {
                Debug.Log("M�O CHEIA DEMAIS");
                yield break;
            }

            if (drawPile.Count == 0)
            {
                if (discardPile.Count == 0)
                {
                    Debug.Log("acabou as cartas");
                    yield break;
                }

                ReturnDiscard();
            }

            yield return DrawCard();
        }
    }

    public IEnumerator DrawCard()
    {
        Card c = drawPile.Draw();
        yield return handManager.AddCard(c);
    }

    private static System.Random rng = new();
    public void ShuffleDeck()
    {

        int n = drawPile.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = drawPile[k];
            drawPile[k] = drawPile[n];
            drawPile[n] = value;
        }
    }

    public void ReturnDiscard()
    {
        drawPile.AddRange(drawPile);
        drawPile.Clear();
        ShuffleDeck();
    }

    public void OnCardPlayed(Card card)
    {
        drawPile.Remove(card);
        drawPile.Add(card);
    }

}
