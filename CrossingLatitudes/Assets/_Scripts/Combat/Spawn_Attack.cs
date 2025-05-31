using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Attack : MonoBehaviour
{
    public GameObject itemAttackPrefab;
    public Vector3 localAttack;

    private void Start()
    {
        //SpawnAttack(localAttack);
    }
    public void SpawnAttack(Vector3 enemyPosition)
    {
        Vector3 spawnPosition = enemyPosition + new Vector3(-1f, 0.5f, 0); // Ajuste conforme necessário
        Instantiate(itemAttackPrefab, spawnPosition, Quaternion.identity);
    }
}
