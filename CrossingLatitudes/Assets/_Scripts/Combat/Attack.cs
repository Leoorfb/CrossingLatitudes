using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public AxeAttack attack;
    public GameObject axePrefab;
    public Transform enemyTarget;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnAxeAttack();
        }
    }

    void SpawnAxeAttack()
    {
        Vector3 spawnPosition = enemyTarget.position + new Vector3(-1f, 0.5f, 0);
        Instantiate(axePrefab, spawnPosition, Quaternion.identity);
    }
}
