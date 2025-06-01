using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private GameObject fireVFX;
    [SerializeField] private GameObject waterVFX;
    [SerializeField] private GameObject electricVFX;

    public Element rewardElement;
    public CardData rewardCard;

    public void OnHealButtonClick()
    {
        HealGA healGA = new(HeroSystem.Instance.HeroView.MaxHealth / 2);
        ActionSystem.Instance.Perform(healGA);
        MatchSetupSystem.Instance.StartNewMatch();
    }

    public void OnGetCardButtonClick()
    {
        CardSystem.Instance.AddCard(rewardCard);
        MatchSetupSystem.Instance.StartNewMatch();
    }

    public void OnAddEffectButtonClick()
    {
        Debug.Log("ADD EFFECT");
    }

    public void SetupReward(CardData cardData, Element element)
    {
        rewardCard = cardData;
        rewardElement = element;

        cardImage.sprite = cardData.sprite;

        fireVFX.SetActive(false);
        waterVFX.SetActive(false);
        electricVFX.SetActive(false);

        switch (element)
        {
            case Element.Fire:
                fireVFX.SetActive(true);
                break;
            case Element.Water:
                waterVFX.SetActive(true);
                break;
            case Element.Electric:
                electricVFX.SetActive(true);
                break;
        }
    }
}
