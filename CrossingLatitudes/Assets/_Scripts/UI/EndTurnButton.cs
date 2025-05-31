using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    public void OnClick()
    {
        if (ActionSystem.Instance.IsPerforming)
            return;

        EnemyTurnGA  enemyTurnGA = new();
        ActionSystem.Instance.Perform(enemyTurnGA);
    }
}
