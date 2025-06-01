using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : Singleton<MatchSetupSystem>
{
    [SerializeField] private HeroData heroData;
    [SerializeField] private List<EnemyData> enemyDatas;

    [SerializeField] private GameObject FightUI;
    [SerializeField] private RewardUI RewardUI;

    private Element rewardElement;
    private CardData rewardCard;

    private void Start()
    {
        HeroSystem.Instance.Setup(heroData);
        EnemySystem.Instance.Setup(enemyDatas);
        CardSystem.Instance.Setup(heroData.Deck);

        SetupReward(enemyDatas[0]);

        DrawCardsGA drawCardsGA = new(3);
        ActionSystem.Instance.Perform(drawCardsGA);
    }

    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<KillEnemyGA>(KillEnemyPostAction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<KillEnemyGA>(KillEnemyPostAction, ReactionTiming.POST);
    }

    // Reactions
    private void KillEnemyPostAction(KillEnemyGA killEnemyGA)
    {
        if (EnemySystem.Instance.enemyBoardView.EnemyViews.Count == 0)
        {
            Debug.Log("INIMIGO DERROTADO - - INICIAR NOVA LUTA");
            FightUI.SetActive(false);

            StartCoroutine(CardSystem.Instance.DiscardAllCards());

            RewardUI.SetupReward(rewardCard, rewardElement);
            RewardUI.gameObject.SetActive(true);
        }
    }

    // Helpers

    public void StartNewMatch()
    {
        RewardUI.gameObject.SetActive(false);
        FightUI.SetActive(true);

        EnemySystem.Instance.Setup(enemyDatas);
        CardSystem.Instance.StartMatchSetup();

        SetupReward(enemyDatas[0]);

        DrawCardsGA drawCardsGA = new(3);
        ActionSystem.Instance.Perform(drawCardsGA);
    }

    private void SetupReward(EnemyData enemyData)
    {
        rewardCard = enemyData.rewardCard;
        rewardElement = enemyData.element;
    }

    private EnemyData GetRandomEnemy()
    {
        return enemyDatas[Random.Range(0, enemyDatas.Count)];
    }
}
