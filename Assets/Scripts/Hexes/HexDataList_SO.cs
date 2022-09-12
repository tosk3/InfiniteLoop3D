using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Hex/HexRarity/newHexRarityList")]
public class HexDataList_SO : ScriptableObject
{
  public List<HexData_SO> dataList;
  public int[] chanceWeights;
}
