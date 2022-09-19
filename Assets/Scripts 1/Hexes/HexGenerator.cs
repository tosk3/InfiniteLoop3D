using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class HexGenerator
{
    public static HexData CreateRandomHex(ManagerHelper helper, HexDataList_SO dataList)
    {
        int hexRarityIndex = TotoUtils.RandomIntBasedOnWeight(dataList.chanceWeights, 0);

        HexData hexData = new HexData()
        {
            rarity = dataList.dataList[hexRarityIndex].rarity,
            hexModel = dataList.dataList[hexRarityIndex].hexModels[UnityEngine.Random.Range(0, dataList.dataList[hexRarityIndex].hexModels.Count)],
            lifeDays = UnityEngine.Random.Range
                (UnityEngine.Random.Range((int)dataList.dataList[hexRarityIndex].minTimeVariance.x, (int)dataList.dataList[hexRarityIndex].minTimeVariance.y),
                (UnityEngine.Random.Range((int)dataList.dataList[hexRarityIndex].maxTimeVariance.x, (int)dataList.dataList[hexRarityIndex].maxTimeVariance.y))),
            crystalAmount = UnityEngine.Random.Range((int)dataList.dataList[hexRarityIndex].crystalAmountVariance.x, (int)dataList.dataList[hexRarityIndex].crystalAmountVariance.y),
            isLooted = false,
        };

        return hexData;
    }
}
