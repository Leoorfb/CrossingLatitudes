using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public EneniesScript enemyData;
    public Element elemento;
    public HealthBarScript healthBarScript;

    private int currentHealth;

    private void Start()
    {
        currentHealth = enemyData.maxHealth;
        healthBarScript.SetMaxHealth(currentHealth);
        Debug.Log(enemyData.enemyName + " spawned with " + currentHealth + " HP");
        elemento = GetRandomElement();
        Debug.Log(enemyData.enemyName + " nasceu com o elemtento: " +  elemento);

    }

    Element GetRandomElement()
    {
        Element[] elements = (Element[])System.Enum.GetValues(typeof(Element));
        return elements[Random.Range(0, elements.Length)];
    }
    private void Recompensa()
    {
        Debug.Log("Esclha entre item ou elemento (" + elemento + ")");

        // pegar o prefab do item enemyData.enemyItem e colocar na ui para o jogador escolher
    }



}
