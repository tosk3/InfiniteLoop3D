using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileInfoDisplay : MonoBehaviour
{
    [SerializeField] private ManagerHelper helper;

    [SerializeField] private GameObject tileInfoCard;
    [SerializeField] private TMP_Text tileName;
    [SerializeField] private TMP_Text tileTimeRemaining;
    [SerializeField] private TMP_Text tileCrystalChance;


    private void Start()
    {
        tileInfoCard.transform.gameObject.SetActive(false);
        helper.selectionManager.OnHexSelection += SelectionManager_OnHexSelection;
    }

    private void SelectionManager_OnHexSelection(object sender, SelectionManager.OnHexSelectionArgs e)
    {
        tileInfoCard.transform.gameObject.SetActive(false);
        if (e.currentSelection == null) return;
        if (e.currentSelection.GetComponent<Hex>() == null) return;

        tileInfoCard.transform.gameObject.SetActive(true);
        tileName.text = "Location Type :" + e.currentSelection.GetComponent<Hex>().GetHexData().rarity.ToString();
        tileTimeRemaining.text = "Time Remaining : " + (e.currentSelection.GetComponent<Hex>().GetHexData().lifeDays - e.currentSelection.GetComponent<Hex>().GetHexData().elapsedDays).ToString();
        switch (e.currentSelection.GetComponent<Hex>().GetHexData().rarity) 
        {
            case HexRarity.abundant:
                tileCrystalChance.text = "Crystal Chance : Low";
                break;
            case HexRarity.normal:
                tileCrystalChance.text = "Crystal Chance : Medium";
                break;
            case HexRarity.rare:
                tileCrystalChance.text = "Crystal Chance : High";
                break;
            default:
                tileCrystalChance.text = "Crystal Chance : Low";
                break;
        }

        

    }
}
