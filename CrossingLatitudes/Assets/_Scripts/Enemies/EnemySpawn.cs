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
    
    public List<EnemySpawnData> enemies;
    public Transform spawnPoint;

    public string spawnEnemyName;
    public GameObject spawnEnemyItem;

    private void Start()
    {
        SpawnRandomEnemy();
    }

    void SpawnRandomEnemy()
    {
        if (enemies.Count == 0)
        {
            Debug.Log("Todos os inimigos foram derrotados!");
            return;
        }

        int randomIndex = Random.Range(0, enemies.Count);
        EnemySpawnData selectedEnemy = enemies[randomIndex];

        GameObject enemy = Instantiate(selectedEnemy.enemyPrefab, spawnPoint.position, Quaternion.identity) as GameObject;
        Enemy enemyScript = enemy.GetComponent<Enemy>();

        enemyScript.enemyData = selectedEnemy.enemyData;

        spawnEnemyName = enemy.name;
        spawnEnemyItem = selectedEnemy.enemyData.enemyItem;

        Debug.Log("Spawned Enemy: " + selectedEnemy.enemyData.enemyName + " with item: " + selectedEnemy.enemyData.enemyItem);
    }


}
