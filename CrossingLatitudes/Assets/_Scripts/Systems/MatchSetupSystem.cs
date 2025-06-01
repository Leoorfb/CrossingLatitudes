using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : Singleton<MatchSetupSystem>
{
    [SerializeField] private HeroData heroData;
    [SerializeField] private List<EnemyData> enemyDatas;
    [SerializeField] private List<EnemyData> bossDatas;

    [SerializeField] private GameObject FightUI;
    [SerializeField] private RewardUI RewardUI;

    private Element rewardElement;
    private CardData rewardCard;

    private List<EnemyData> currentEnemies = new();

    private bool isBossMatch = false;

    private void Start()
    {
        NewCurrentEnemy();

        HeroSystem.Instance.Setup(heroData);
        EnemySystem.Instance.Setup(currentEnemies);
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

            if (isBossMatch)
            {
                Debug.Log("Fim de jogo - VITORIA");
                return;
            }

            RewardUI.SetupReward(rewardCard, rewardElement);
            RewardUI.gameObject.SetActive(true);
        }
    }

    // Helpers

    public void StartNewMatch()
    {
        RewardUI.gameObject.SetActive(false);
        FightUI.SetActive(true);

        NewCurrentEnemy();

        EnemySystem.Instance.Setup(currentEnemies);
        CardSystem.Instance.StartMatchSetup();

        SetupReward(currentEnemies[0]);

        DrawCardsGA drawCardsGA = new(3);
        ActionSystem.Instance.Perform(drawCardsGA);
    }

    private void SetupReward(EnemyData enemyData)
    {
        rewardCard = enemyData.rewardCard;
        rewardElement = enemyData.element;
    }

    private void NewCurrentEnemy()
    {
        int randIndex;
        currentEnemies.Clear();
        if (enemyDatas.Count <= 0)
        {
            isBossMatch = true;
            randIndex = Random.Range(0, bossDatas.Count);
            currentEnemies.Add(bossDatas[randIndex]);
            return;
        }

        randIndex = Random.Range(0, enemyDatas.Count);
        currentEnemies.Add(enemyDatas[randIndex]);
        enemyDatas.RemoveAt(randIndex);
        return;
    }
}
