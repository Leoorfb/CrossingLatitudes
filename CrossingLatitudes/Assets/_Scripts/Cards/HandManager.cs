using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class HandManager : Singleton<HandManager>
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private Transform spawnPoint;

    private List<CardView> handCards = new();


    public CardView RemoveCard(Card c)
    {
        foreach (CardView cardV in handCards)
        {
            if (cardV.card == c)
            {
                handCards.Remove(cardV);
                StartCoroutine(UpdateCardsPositions(0.2f));
                return cardV;
            }
        }

        return null;
    }

    public IEnumerator AddCard(Card c)
    {

        if (c == null) yield break;

        GameObject g = Instantiate(cardPrefab, spawnPoint.position, spawnPoint.rotation);
        CardView cv = g.GetComponent<CardView>();

        cv.Setup(c);

        handCards.Add(cv);

        yield return UpdateCardsPositions(.2f);
    }

    public IEnumerator OnCardPlayed(CardView cardV)
    {
        foreach (CardView cv in handCards)
        {
            if (cv == cardV)
            {
                handCards.Remove(cv);
                break;
            }
        }

        yield return UpdateCardsPositions(0.2f);
    }

    private IEnumerator UpdateCardsPositions(float duration)
    {
        if (handCards.Count == 0)
            yield break;

        float cardSpacing = 1f / CardSystem.Instance.maxHandSize;
        float firstCardPosition = .5f - (handCards.Count - 1) * cardSpacing / 2;

        Spline spline = splineContainer.Spline;
        for (int i = 0; i < handCards.Count; i++)
        {
            float p = firstCardPosition + i *  cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward =  spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);

            handCards[i].transform.DOMove(splinePosition, duration);
            handCards[i].transform.DOLocalRotateQuaternion(rotation, duration);
            handCards[i].setSortLayer(i+1);
        }
        yield return new WaitForSeconds(duration);
    }

}
