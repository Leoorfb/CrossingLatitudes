using SerializeReferenceEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/CardData")]
public class CardData : ScriptableObject
{
    [field: SerializeField] public Sprite sprite { get; private set; }
    [field: SerializeField] public int cost { get; private set; }

    [field: SerializeReference, SR]    public List<EffectPlain> effects { get; private set; }

    //[field: SerializeField] public string effect { get; private set; }
}
