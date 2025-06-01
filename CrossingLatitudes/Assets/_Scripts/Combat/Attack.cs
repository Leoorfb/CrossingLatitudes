using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public EnemySpawn enemySpawn;
    public Enemy currentEnemy;
    
    public GameObject axePrefab;
    public Transform enemyTarget;

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && enemySpawn.isCombatActive == true)
        {
            if (currentEnemy != null)
            {
                PlayerAttack();
                SpawnAxeAttack();
            }
            else
            {
                Debug.Log("Não há inimigos para atacar.");
            }
        }
        
    }
    void PlayerAttack()
    {
        int damage = 10;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindEnemyInScene();
            Debug.Log("Player atacou o inimigo por " + damage + " de dano");
            SpawnAxeAttack();
            currentEnemy.TakeDamage(damage);
        }
    }
    void SpawnAxeAttack()
    {
        Vector3 spawnPosition = enemyTarget.position + new Vector3(-1f, 0.5f, 0);
        Instantiate(axePrefab, spawnPosition, Quaternion.identity);
    }

    public void FindEnemyInScene()
    {
        currentEnemy = FindObjectOfType<Enemy>();

        if (currentEnemy != null)
        {
            Debug.Log("Inimigo encontrado: " + currentEnemy.enemyData.enemyName);
        }
        else
        {
            Debug.LogWarning("Nenhum inimigo encontrado na cena!");
        }
    }
}
