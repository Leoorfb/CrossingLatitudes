using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSystem : Singleton<HeroSystem>
{
    [field: SerializeField] public UnitView HeroView { get; private set; }

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<AddAttackGA>(AddAttackPerformer);
        ActionSystem.AttachPerformer<AddDefenseGA>(AddDefensePerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreAction, ReactionTiming.PRE);

    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<AddAttackGA>();
        ActionSystem.DetachPerformer<AddDefenseGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreAction, ReactionTiming.PRE);

    }

    public void Setup(HeroData heroData)
    {
        HeroView.Setup(heroData.MaxHealth,heroData.Image, heroData.AttackPower, heroData.DefensePower);
    }

    // Reactions

    private void EnemyTurnPreAction(EnemyTurnGA enemyTurnGA)
    {
        UnitAttackGA unitAttackGA = new(HeroView, EnemySystem.Instance.enemyBoardView.EnemyViews[0]);
        ActionSystem.Instance.AddReaction(unitAttackGA);
    }

    // Performer 

    private IEnumerator AddAttackPerformer(AddAttackGA addDamageGA)
    {
        HeroView.AddAttack(addDamageGA.amount);
        yield return null;
    }

    private IEnumerator AddDefensePerformer(AddDefenseGA addDefenseGA)
    {
        HeroView.AddDefense(addDefenseGA.amount);
        yield return null;
    }
}
