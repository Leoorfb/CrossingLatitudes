using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text defenseText;

    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    public int BaseAttackPower { get; private set; }
    public int AttackPower { get; private set; }
    public int BaseDefensePower { get; private set; }
    public int DefensePower { get; private set; }

    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostAction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostAction, ReactionTiming.POST);
    }

    public void Setup(int health, Sprite image, int attack, int defense)
    {
        MaxHealth = CurrentHealth = health;
        spriteRenderer.sprite = image;
        BaseAttackPower = AttackPower = attack;
        BaseDefensePower = DefensePower = defense;

        UpdateHealthText();
        UpdateAttackText();
        UpdateDefenseText();
    }

    private void UpdateHealthText()
    {
        healthText.text = CurrentHealth + "/" + MaxHealth;
    }
    private void UpdateAttackText()
    {
        attackText.text = AttackPower.ToString();
    }
    private void UpdateDefenseText()
    {
        defenseText.text = DefensePower.ToString() ;
    }

    public void Damage(int damageAmount)
    {
        if (DefensePower > 0)
        {
            if (DefensePower > damageAmount)
            {
                DefensePower -= damageAmount;
                damageAmount = 0;
            }
            else
            {
                damageAmount -= DefensePower;
                DefensePower = 0;
            }

            UpdateDefenseText();
        }

        CurrentHealth -= damageAmount;
        if(CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }

        transform.DOShakePosition(0.2f, 0.5f);
        UpdateHealthText();
    }

    public void AddAttack(int attackAmount)
    {
        AttackPower += attackAmount;
        UpdateAttackText();
    }

    public void AddDefense(int defenseAmount)
    {
        DefensePower += defenseAmount;
        UpdateDefenseText();
    }

    // Reactions

    private void EnemyTurnPostAction(EnemyTurnGA enemyTurnGA)
    {
        AttackPower = BaseAttackPower;
        DefensePower = BaseDefensePower;
        UpdateAttackText();
        UpdateDefenseText();
    }
}
