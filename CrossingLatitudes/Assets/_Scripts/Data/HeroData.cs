using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/HeroData")]
public class HeroData : UnitData
{
    [field: SerializeField] public List<CardData> Deck { get; private set; }
}
