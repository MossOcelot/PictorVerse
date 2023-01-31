using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Update_Sell_Account : MonoBehaviour
{
    [SerializeField]
    private GameObject background;
    [SerializeField]
    public Text total_text;
    Button confirm_sell;
    int total_sell;
    // Start is called before the first frame update
    void Start()
    {
        confirm_sell = gameObject.GetComponent<Button>();
        confirm_sell.AddEventListener(1, OnClickConfirmSell);
    }

    private void Update()
    {
        total_sell = (int)((float)background.gameObject.GetComponent<Shop_manager>().getSellPrice() * 0.7f);
        total_text.text = (total_sell).ToString();
    }

    void OnClickConfirmSell(int i)
    {
        background.gameObject.GetComponent<Shop_manager>().changeSellPrice(0);
    }
}
