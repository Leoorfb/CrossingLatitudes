    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyViewCreator : Singleton<EnemyViewCreator>
{
    [SerializeField] private UnitView enemyViewPrefab;

    public UnitView CreateEnemyView(EnemyData enemyData, Vector3 postion, Quaternion rotation)
    {
        UnitView enemyView = Instantiate(enemyViewPrefab, postion, rotation);
        enemyView.Setup(enemyData.MaxHealth, enemyData.Image, enemyData.AttackPower, enemyData.DefensePower);
        return enemyView;
    }
}
