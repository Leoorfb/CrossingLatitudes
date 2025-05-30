using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Enemy", menuName ="Enemy/Create new Enemy")]
public class EneniesScript : ScriptableObject
{
    public string enemyName;
    public int maxHealth;
    public int damage;
    public int defense;
    public GameObject enemyPrefab;
    
}
