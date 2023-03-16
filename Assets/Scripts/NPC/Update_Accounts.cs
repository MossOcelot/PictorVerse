using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Update_Accounts : MonoBehaviour
{
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private TextMeshProUGUI price_text;
    [SerializeField]
    private TextMeshProUGUI vat_text;
    [SerializeField]
    private TextMeshProUGUI total_text;
    [SerializeField]
    private TextMeshProUGUI Balance;

    Button ConfirmBuy;
    private void FixedUpdate()
    {
        float[] accounts = background.gameObject.GetComponent<Shop_manager>().getAccounts();
        price_text.text = accounts[0].ToString("F");
        vat_text.text = accounts[1].ToString("F"); 
        total_text.text = accounts[2].ToString("F");

        if (accounts[2] > float.Parse(Balance.text))
        {
            gameObject.GetComponent<Button>().interactable = false;
        } else
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
    }
}
