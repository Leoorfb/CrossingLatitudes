using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    public Attack attackScript;
    public GameObject spawnEnemyItem;

    public string spawnEnemyName;
    public bool isCombatActive = false;

    private List<int> usedIndexes = new List<int>();
    private void Start()
    {
        SpawnRandomEnemy();
    }

    void SpawnRandomEnemy()
    {
        if (usedIndexes.Count >= enemies.Count)
        {
            Debug.Log("Todos os inimigos foram derrotados!");
            SceneManager.LoadScene("Boss");
            return;
        }


        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, enemies.Count);
        } while (usedIndexes.Contains(randomIndex));

        usedIndexes.Add(randomIndex);

        
        EnemySpawnData selectedEnemy = enemies[randomIndex];

        GameObject enemy = Instantiate(selectedEnemy.enemyPrefab, spawnPoint.position, Quaternion.identity) as GameObject;
        isCombatActive = true;
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        
        enemyScript.enemyData = selectedEnemy.enemyData;

        spawnEnemyName = enemy.name;
        spawnEnemyItem = selectedEnemy.enemyData.enemyItem;
        attackScript.currentEnemy = enemyScript; // faz com que o inimigo spawnado atualmente recebe o ataque
        enemyScript.OnEnemyDefeated += OnEnemyDefeated;
        Debug.Log("Spawned Enemy: " + selectedEnemy.enemyData.enemyName + " with item: " + selectedEnemy.enemyData.enemyItem);

    }

    private void HandleEnemyDefeated(EneniesScript defeatedEnemy)
    {
        
        enemies.RemoveAll(e => e.enemyData == defeatedEnemy);

        
        SpawnRandomEnemy();
    }
    private void OnEnemyDefeated(EneniesScript enemyData)
    {
        Debug.Log(enemyData.enemyName + " foi derrotado.");

        // Desinscreve do evento para evitar refer�ncia pendurada
        attackScript.currentEnemy.OnEnemyDefeated -= OnEnemyDefeated;

        // Spawn do pr�ximo inimigo
        SpawnRandomEnemy();
    }
    
}
