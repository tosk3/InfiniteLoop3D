using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_CrystalDisplay : MonoBehaviour
{
    [SerializeField] private ManagerHelper helper;
    [SerializeField] private DayCycle dayManager;

    [SerializeField] private Image dayImage;
    [SerializeField] private TMP_Text dayCountText;


    // Start is called before the first frame update
    void Start()
    {
        dayManager = helper.dayManager;
        dayManager.OnDayPassed += DayManager_OnDayPassed;
        helper.playerPawn.OnCrystalHarvest += PlayerPawn_OnCrystalHarvest;
        dayCountText.text = "Days remaining : " + (int)helper.playerPawn.GetRemainingCrystalTime();
    }

    private void PlayerPawn_OnCrystalHarvest(object sender, PlayerPawn.OnCrystalHarvestArgs e)
    {
        dayCountText.text = "Days remaining : " + (int)helper.playerPawn.GetRemainingCrystalTime();
    }

    private void DayManager_OnDayPassed(object sender, DayCycle.OnDayPassedArgs e)
    {
        dayCountText.text = "Days remaining : " + (int)helper.playerPawn.GetRemainingCrystalTime();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dayImage.rectTransform.localScale = new Vector3(1 - dayManager.DayProgress(), 1 , 1);
    }
}
