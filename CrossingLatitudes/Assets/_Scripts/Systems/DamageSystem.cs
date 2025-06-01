using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField] private GameObject damageVFX;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DealDamageGA>();
    }

    private IEnumerator DealDamagePerformer(DealDamageGA dealDamageGA)
    {
        foreach (var target in dealDamageGA.Targets)
        {
            target.Damage(dealDamageGA.Amount);
            Instantiate(damageVFX, target.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.15f);

            if (target.CurrentHealth <= 0)
            {
                if (EnemySystem.Instance.enemyBoardView.EnemyViews.Contains(target))
                {
                    KillEnemyGA killEnemyGA = new(target);
                    ActionSystem.Instance.AddReaction(killEnemyGA);
                }
                else
                {
                    Debug.Log("GAME OVER");
                }
            }
        }
        yield return new WaitForSeconds(0.4f);
    }
}
