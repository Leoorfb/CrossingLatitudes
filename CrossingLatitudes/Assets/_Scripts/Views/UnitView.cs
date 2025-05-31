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
    public int AttackPower { get; private set; }
    public int DefensePower { get; private set; }

    public void Setup(int health, Sprite image, int attack, int defense)
    {
        MaxHealth = CurrentHealth = health;
        spriteRenderer.sprite = image;
        AttackPower = attack;
        DefensePower = defense;

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
}
