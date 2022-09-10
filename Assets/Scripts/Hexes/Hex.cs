using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
   
}

public struct HexData
{
    public HexRarity rarity;
    public GameObject hexModels;
    //public List<GameObject> lootTables;
    public Vector2 minTimeVariance;
    public Vector2 maxTimeVariance;
}
