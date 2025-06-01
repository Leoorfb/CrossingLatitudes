using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardUI : MonoBehaviour
{
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
    }
}
