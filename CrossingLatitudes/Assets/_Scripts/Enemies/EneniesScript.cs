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
    public string element;
    public GameObject enemyPrefab;
    public GameObject enemyItem;
}

[CreateAssetMenu(fileName = "New Boss", menuName = "Boss/Create a new Boss")]
public class BossScript : ScriptableObject
{
    public string bossName;
    public int maxHealth;
    public int damage;
    public string element;
    
}