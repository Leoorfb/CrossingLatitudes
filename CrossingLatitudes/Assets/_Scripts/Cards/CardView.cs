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

    private Card card;

    public void Setup(Card card)
    {
        Debug.Log("card setup");
        this.card = card;
        cardImage.sprite = card.sprite;
        title.text = card.title;
        cost.text = card.cost.ToString();
        description.text = card.effect.ToString();
    }

    private void OnMouseDown()
    {
        card.PerformEffect();
        Destroy(gameObject);
    }

}
