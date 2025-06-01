using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class CardView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cardImage;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text cost;
    [SerializeField] private TMP_Text description;
    [SerializeField] private GameObject wrapper;
    [SerializeField] private LayerMask dropLayer;
    [SerializeField] private SortingGroup sortingGroup;

    public Card card { get; private set; }

    private Vector3 dragStartPosition;
    private Quaternion dragStartRotation;

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

    public void setSortLayer(int layer)
    {
        sortingGroup.sortingOrder = layer;
    }

    private void OnMouseDown()
    {
        if (!Interactions.Instance.PlayerCanInteract())
            return;

        Interactions.Instance.PlayerIsDragging = true;
        
        wrapper.SetActive(true);
        CardViewHoverSystem.Instance.Hide();

        dragStartPosition = transform.position;
        dragStartRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
    }

    private void OnMouseDrag()
    {
        if (!Interactions.Instance.PlayerCanInteract())
            return;

        transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
    }

    private void OnMouseUp()
    {
        if (!Interactions.Instance.PlayerCanInteract())
            return;

        if (ManaSystem.Instance.HasEnoughMana(card.cost)
            && Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, 10f, dropLayer))
        {
            Debug.Log("usou");

            PlayCardGA playcardGA = new(card);
            ActionSystem.Instance.Perform(playcardGA);
        }
        else
        {
            Debug.Log("cancelou");

            transform.position = dragStartPosition;
            transform.rotation = dragStartRotation;
        }
        

        Interactions.Instance.PlayerIsDragging = false;
    }

    private void OnMouseEnter()
    {
        if (!Interactions.Instance.PlayerCanHover())
            return;
        wrapper.SetActive(false);
        Vector3 pos = new(transform.position.x, -2, 0);
        CardViewHoverSystem.Instance.Show(card, pos);
    }

    private void OnMouseExit()
    {
        if (!Interactions.Instance.PlayerCanHover())
            return;
        CardViewHoverSystem.Instance.Hide();
        wrapper.SetActive(true);
    }

}
