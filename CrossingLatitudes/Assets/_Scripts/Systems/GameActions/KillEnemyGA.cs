using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyGA : GameAction
{
    public UnitView EnemyView { get; private set; }

    public KillEnemyGA(UnitView enemyView)
    {
        EnemyView = enemyView;
    }
}
