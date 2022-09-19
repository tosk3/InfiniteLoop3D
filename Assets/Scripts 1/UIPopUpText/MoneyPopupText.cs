using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyPopupText : MonoBehaviour
{
    //static create money object, Instatiate at given location
    public static MoneyPopupText Create(Vector3 position, Quaternion rotation, Transform pfMoneyPopUp, Transform parent)
    {
        Transform moneyPopupTextTransform = Instantiate(pfMoneyPopUp, position, rotation, parent);

        MoneyPopupText moneyPopupText = moneyPopupTextTransform.GetComponent<MoneyPopupText>();

        return moneyPopupText;
    }

    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private float disappearTimer;
    [SerializeField] private float disappearingSpeed;
    [SerializeField] private Color textColor;
    [SerializeField] private float moveSpeed;


    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>(); 
    }
    public void Setup(int crystalAmount, float disappearTimer, float moveSpeed, float disappearingSpeed,bool isLooted)
    {
       //check if looted
        if (isLooted) return;
        //check for amount of crystal received
        if(crystalAmount > 0 )
        {
            //if not exploded add +
            textMesh.text = "+" + crystalAmount.ToString();
            //change color to gold ?
            textColor = textMesh.color;
        }
        else
        {
            //else nothing
            textMesh.text = "N/a";
            textColor = textMesh.color;
        }

        //setup variables
        this.disappearTimer = disappearTimer;
        this.moveSpeed = moveSpeed;
        this.disappearingSpeed = disappearingSpeed;
    }

    //run update on the text object. Move up, calculate when to disappear and when to destroy self.
    private void Update()
    {
        transform.position += new Vector3(0f, moveSpeed) * Time.deltaTime;
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            textColor.a -= disappearingSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
