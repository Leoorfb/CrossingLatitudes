using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public EneniesScript enemyData;
    public Element elemento;
    public int currentHealth;
    public event Action<EneniesScript> OnEnemyDefeated;


    private void Start()
    {
        currentHealth = enemyData.maxHealth;
        elemento = GetRandomElement();
        Debug.Log(enemyData.enemyName + " nasceu com o elemtento: " +  elemento);

    }

    Element GetRandomElement()
    {
        Element[] elements = (Element[])System.Enum.GetValues(typeof(Element));
        return elements[UnityEngine.Random.Range(0, elements.Length)];
    }
    private void Recompensa()
    {
        Debug.Log("Esclha entre o item: " + enemyData.enemyItem + " ou o elemento: " + elemento );

        // pegar o prefab do item enemyData.enemyItem e colocar na ui para o jogador escolher
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(enemyData.name  + "recebeu dano: " + damage +" | "+ currentHealth + " de vida restante");
        if(currentHealth <= 0)
        {
            Die();
        }
        
    }

    private void Die()
    {
        Debug.Log(enemyData.enemyName + " morreu!");

        Recompensa();

        OnEnemyDefeated?.Invoke(enemyData); // Notifica o spawner

        Destroy(gameObject);
    }

}
