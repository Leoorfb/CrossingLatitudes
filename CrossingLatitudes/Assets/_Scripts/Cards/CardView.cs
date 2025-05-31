using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cardImage;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text cost;
    [SerializeField] private TMP_Text description;
    [SerializeField] private GameObject wrapper;

    private Card card;

    public void Setup(Card card)
    {
        //Debug.Log("card setup");
        this.card = card;
        cardImage.sprite = card.sprite;
        title.text = card.title;
        cost.text = card.cost.ToString();

        string desc = "";
        foreach (var effect in card.effects)
        {
            desc += effect.GetDescription();
        }

        description.text = desc;
    }

    private void OnMouseDown()
    {
        if (ActionSystem.Instance.IsPerforming) return;
        card.PerformEffect();

        DeckManager.Instance.OnCardPlayed(card);
        HandManager.Instance.OnCardPlayed(this);
        Destroy(gameObject);
    }

    private void OnMouseEnter()
    {
        wrapper.SetActive(false);
        Vector3 pos = new(transform.position.x, -2, 0);
        CardViewHoverSystem.Instance.Show(card, pos);
    }

    private void OnMouseExit()
    {
        CardViewHoverSystem.Instance.Hide();
        wrapper.SetActive(true);
    }
}
