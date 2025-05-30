using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EneniesScript enemyData;
    public HealthBarScript healthBarScript;

    private int currentHealth;

    private void Start()
    {
        currentHealth = enemyData.maxHealth;
        healthBarScript.SetMaxHealth(currentHealth);
        Debug.Log(enemyData.enemyName + " spawned with " + currentHealth + " HP");

    }

}
