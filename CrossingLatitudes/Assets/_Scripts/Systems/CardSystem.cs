using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{
    [SerializeField] public int maxHandSize;
    [SerializeField] private HandManager handManager;

    [SerializeField] Transform drawCardPosition;
    [SerializeField] Transform discardCardPosition;

    private  List<Card> drawPile = new();
    private  List<Card> discardPile = new();
    private  List<Card> handPile = new();

    [SerializeField] public int cardsDrawnPerTurn = 2;

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
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostAction, ReactionTiming.POST);

    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardsGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostAction, ReactionTiming.POST);

    }

    // Performers

    private IEnumerator DrawCardPerformer(DrawCardsGA drawCardsGA)
    {
        for(int i = 0; i < drawCardsGA.amount; i++)
        {
            if (handPile.Count >= maxHandSize)
            {
                Debug.Log("MÃO CHEIA DEMAIS");
                yield break;
            }

            if (drawPile.Count == 0)
            {
                Debug.Log("baralho vazio");

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


    private IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
    {
        handPile.Remove(playCardGA.Card);
        discardPile.Add(playCardGA.Card);

        CardView cardV = HandManager.Instance.RemoveCard(playCardGA.Card);

        yield return DiscardCard(cardV);

        SpendManaGA spendManaGA = new(playCardGA.Card.cost);
        ActionSystem.Instance.AddReaction(spendManaGA);

        foreach(EffectPlain effect in playCardGA.Card.effects)
        {
            Debug.Log("efeito da carta " + effect.GetDescription());
            PerformEffectGA performEffectGA = new(effect);
            ActionSystem.Instance.AddReaction(performEffectGA);
        }

        //yield return playCardGA.Card.PerformEffect();

    }

    // Reactions

    private void EnemyTurnPostAction(EnemyTurnGA enemyTurnGA)
    {
        DrawCardsGA drawCardsGA = new(cardsDrawnPerTurn);
        ActionSystem.Instance.AddReaction(drawCardsGA);
    }

    // Helpers

    public void Setup(List<CardData> deck)
    {
        foreach (var cardData in deck)
        {
            AddCard(cardData);
        }

        ShuffleDeck();
    }

    public void StartMatchSetup()
    {
        ReturnDiscard();
    }

    public void AddCard(CardData cardData)
    {
        Card card = new(cardData);
        drawPile.Add(card);
    }

    public IEnumerator DrawCard()
    {
        Card c = drawPile.Draw();
        handPile.Add(c);
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
        Debug.Log("retornou o discarte");

        drawPile.AddRange(discardPile);
        discardPile.Clear();
        ShuffleDeck();
    }

    private IEnumerator DiscardCard(CardView cardView)
    {
        Debug.Log(cardView);
        Debug.Log(cardView.transform);

        cardView.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardView.transform.DOMove(discardCardPosition.position, 0.15f);
        yield return tween.WaitForCompletion();

        Destroy(cardView.gameObject);
    }

    public IEnumerator DiscardAllCards()
    {
        while(handPile.Count > 0)
        {
            discardPile.Add(handPile[0]);

            CardView cardV = HandManager.Instance.RemoveCard(handPile[0]);
            handPile.Remove(handPile[0]);


            yield return DiscardCard(cardV);
        }
    }
}
