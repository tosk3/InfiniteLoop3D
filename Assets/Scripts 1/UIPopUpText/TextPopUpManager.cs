using UnityEngine;

public class TextPopUpManager : MonoBehaviour 
{
    [SerializeField] private ManagerHelper helper;
    [SerializeField] private UI_PrefabList_SO UI_PrefabList;
    [SerializeField] private UI_Data_SO UI_Data;

    private void Start()
    {
        helper.playerPawn.OnCrystalHarvest += Player_OnCrytalHarvest;
        UI_PrefabList = Resources.Load<UI_PrefabList_SO>(typeof(UI_PrefabList_SO).Name.ToString()); // load a resource file, the prefablist
        UI_Data = Resources.Load<UI_Data_SO>(typeof(UI_Data_SO).Name.ToString());//load UI data file
    }

    private void Player_OnCrytalHarvest(object sender, PlayerPawn.OnCrystalHarvestArgs e)
    {
        CreateMoneyPopupText(e.playerHexPosition.transform.position,(int)e.harvestedCrystalAmount,e.isLooted);
    }

    public void CreateMoneyPopupText(Vector3 position,int amount,bool isLooted)
    {
        //create the world text on the location
        Vector3 relativePos = position - Camera.main.transform.position;
        MoneyPopupText moneyText = MoneyPopupText.Create(position + UI_Data.positionOffset, Quaternion.LookRotation(relativePos, Vector3.up), UI_PrefabList.pf_MoneyText, this.transform);
        //setup the text object with timer, speeds and text
        moneyText.Setup(amount, UI_Data.disappearTimerLength, UI_Data.moveUpSpeed, UI_Data.disappearingSpeed,isLooted);
    }

}
