using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{
    [field: SerializeField] public EnemyBoardView enemyBoardView { get; private set; }

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerformer);
        ActionSystem.AttachPerformer<UnitAttackGA> (UnitAttackPerformer);
        ActionSystem.AttachPerformer<KillEnemyGA> (KillEnemyPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<EnemyTurnGA>();
        ActionSystem.DetachPerformer<UnitAttackGA>();
        ActionSystem.DetachPerformer<KillEnemyGA>();
    }

    public void Setup(List<EnemyData> enemyDatas)
    {
        foreach (EnemyData enemyData in enemyDatas)
        {
            enemyBoardView.AddEnemy(enemyData);
        }
    }

    private IEnumerator EnemyTurnPerformer(EnemyTurnGA enemyTurnGA)
    {
        foreach (var enemy in enemyBoardView.EnemyViews)
        {
            UnitAttackGA unitAttackGA = new(enemy, HeroSystem.Instance.HeroView);
            ActionSystem.Instance.AddReaction(unitAttackGA);
        }
        yield return null;
    }

    private IEnumerator UnitAttackPerformer(UnitAttackGA unitAttackGA)
    {
        UnitView attacker = unitAttackGA.Attacker;
        UnitView target = unitAttackGA.Target;

        int direction = attacker.transform.position.x > target.transform.position.x ? -1 : 1;

        Tween tween = attacker.transform.DOMoveX(attacker.transform.position.x + direction, 0.15f);
        yield return tween.WaitForCompletion();
        attacker.transform.DOMoveX(attacker.transform.position.x - direction, 0.15f);

        DealDamageGA dealDamageGA = new(attacker.AttackPower, new() { target });
        ActionSystem.Instance.AddReaction(dealDamageGA);

    }

    private IEnumerator KillEnemyPerformer(KillEnemyGA killEnemyGA)
    {
        yield return enemyBoardView.RemoveEnemy(killEnemyGA.EnemyView);
    }
}
