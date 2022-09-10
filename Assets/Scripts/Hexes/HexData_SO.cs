using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HexRarity
{
    abundant,
    normal,
    rare
}

[CreateAssetMenu(menuName ="Hex/HexRarity/NewRarity")]
public class HexData_SO : ScriptableObject
{
    public HexRarity rarity;
    public List<GameObject> hexModels;
    //public List<GameObject> lootTables;
    public Vector2 minTimeVariance;
    public Vector2 maxTimeVariance;
}
