using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hex : MonoBehaviour
{
    [SerializeField] private ManagerHelper helper;

    [SerializeField] private HexData m_hexData;
    public void SetupHex(ManagerHelper helper,HexData hexData)
    {
        this.helper = helper;
        helper.dayManager.OnDayPassed += DayManager_OnDayPassed;
        m_hexData = hexData;
        GameObject childObj = Instantiate(m_hexData.hexModel, Vector3.zero, Quaternion.identity, this.transform);
        childObj.transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    private void DayManager_OnDayPassed(object sender, DayCycle.OnDayPassedArgs e)
    {
        ProcessDay();
    }

    private void ProcessDay()
    {
        m_hexData.elapsedDays++;
        if (m_hexData.elapsedDays < m_hexData.lifeDays) return;
        //object pool maybe ?
        helper.dayManager.OnDayPassed -= DayManager_OnDayPassed;
        Destroy(this.gameObject);
    }
    
    public int GetRemainingLifeDays()
    {
        return m_hexData.lifeDays - m_hexData.elapsedDays;
    }
    public void LootHex()
    {
        m_hexData.isLooted = true;
    }
    public HexData GetHexData()
    {
        return m_hexData;
    }
}

[Serializable]
public struct HexData
{
    public HexRarity rarity;
    public GameObject hexModel;
    public bool isLooted;
    public int crystalAmount;
    public int lifeDays;
    public int elapsedDays;
}
