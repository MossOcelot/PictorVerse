using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Update_Accounts : MonoBehaviour
{
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private Text price_text;
    [SerializeField]
    private Text vat_text;
    [SerializeField]
    private Text total_text;
    [SerializeField]
    private Text Balance;

    Button ConfirmBuy;
    private void FixedUpdate()
    {
        float[] accounts = background.gameObject.GetComponent<Shop_manager>().getAccounts();
        price_text.text = accounts[0].ToString();
        vat_text.text = accounts[1].ToString(); 
        total_text.text = accounts[2].ToString();

        if (accounts[2] > float.Parse(Balance.text))
        {
            gameObject.GetComponent<Button>().interactable = false;
        } else
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
    }
}
