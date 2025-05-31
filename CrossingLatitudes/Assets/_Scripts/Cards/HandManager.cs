using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class HandManager : Singleton<HandManager>
{

    [SerializeField] private int maxHandSize;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private Transform spawnPoint;

    private List<CardView> handCards = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ActionSystem.Instance.IsPerforming) return;

            DrawCardGA drawCardGA = new();
            ActionSystem.Instance.Perform(drawCardGA);
        }
    }

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<DrawCardGA>(DrawCardPerformer);
    }

    private IEnumerator DrawCardPerformer(DrawCardGA drawCardGA)
    {

        if (handCards.Count >= maxHandSize)
        {
            Debug.Log("MÃO CHEIA DEMAIS");
            yield break;
        }

        Card c = DeckManager.Instance.DrawCard();

        if (c == null) yield break;

        GameObject g = Instantiate(cardPrefab, spawnPoint.position, spawnPoint.rotation);
        CardView cv = g.GetComponent<CardView>();

        cv.Setup(c);

        handCards.Add(cv);

        UpdateCardsPositions();
    }

    public void OnCardPlayed(CardView cardV)
    {
        foreach (CardView cv in handCards)
        {
            if (cv == cardV)
            {
                handCards.Remove(cv);
                break;
            }
        }

        UpdateCardsPositions();
    }

    private void UpdateCardsPositions()
    {
        if (handCards.Count == 0)
            return;

        float cardSpacing = 1f / maxHandSize;
        float firstCardPosition = .5f - (handCards.Count - 1) * cardSpacing / 2;

        Spline spline = splineContainer.Spline;
        for (int i = 0; i < handCards.Count; i++)
        {
            float p = firstCardPosition + i *  cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward =  spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);

            handCards[i].transform.DOMove(splinePosition, 0.25f);
            handCards[i].transform.DOLocalRotateQuaternion(rotation, 0.25f);
        }
    }

}
