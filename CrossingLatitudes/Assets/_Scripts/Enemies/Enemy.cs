using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public EneniesScript enemyData;
    public Element elemento;
    

    

    private void Start()
    {
        
        
        
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
