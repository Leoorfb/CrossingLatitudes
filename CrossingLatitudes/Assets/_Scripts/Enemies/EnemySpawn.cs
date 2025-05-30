using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemySpawnData
{
    public EneniesScript enemyData;
    public GameObject enemyPrefab;
}
public class EnemySpawn : MonoBehaviour
{
    public EnemySpawnData[] enemies; // lista com os inimigos + prefabs e dados
    public Transform spawnPoint;

    private void Start()
    {
        SpawnRandomEnemy();
    }

    void SpawnRandomEnemy()
    {
        int randomIndex = Random.Range(0, enemies.Length);
        EnemySpawnData selectedEnemy = enemies[randomIndex];

        GameObject enemy = Instantiate(selectedEnemy.enemyPrefab, spawnPoint.position, Quaternion.identity) as GameObject;
        Enemy enemyScript = enemy.GetComponent<Enemy>();
     

        Debug.Log("Spawned Enemy: " + selectedEnemy.enemyData.enemyName);
    }
}
