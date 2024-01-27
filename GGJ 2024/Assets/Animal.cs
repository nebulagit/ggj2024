using System;
using UnityEngine;

[Serializable]
public class Animal
{
    [SerializeField] string species; public string Species { get => species; }
    [Range(1, 5)]
    [SerializeField] int rarity; public int Rarity { get => rarity; }
    [SerializeField] GameObject prefab; public GameObject Prefab { get => prefab; }
    [Space(30)]
    [Range(2, 6)]
    [SerializeField] int baseHappyness; public int BaseHappyness { get => baseHappyness; }
    [Space(0)]
    [Range(2, 6)]
    [SerializeField] int baseBreath; public int BaseBreath { get => baseBreath; }
    [Range(2, 6)]
    [SerializeField] int baseAnger; public int BaseAnger { get => baseAnger; }
    [Range(3, 8)]
    [SerializeField] int tickleSpots; public int TickleSpots { get => tickleSpots; }
    [SerializeField] Color[] avaiableColors; public Color[] AvaiableColors { get => avaiableColors; }
}