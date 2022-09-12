using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hex/HexData/newHexSpawnerData")]
public class HexSpawnerData_SO : ScriptableObject
{
    public GameObject hexPrefab;
    public float gridXPosOffset;
    public float gridYPosOffset;

    public int[] chanceOfSpawnWeight;
    public bool[] chanceOfSpawn;
}
