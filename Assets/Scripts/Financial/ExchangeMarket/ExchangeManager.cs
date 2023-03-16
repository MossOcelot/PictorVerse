using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using TMPro;
public class ExchangeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public CentralBankStatus centralBankStatus;
    public PlayerStatus player;
    public PlayerMovement player_movement;

    [SerializeField]
    private ExchangeRate exchangeRate;

    public TMPro.TMP_Dropdown fromCurrencys;
    public TMPro.TMP_Dropdown toCurrencys;

    public TextMeshProUGUI[] title_fromCurrency;
    public TextMeshProUGUI[] fromCurrency_text;
    public TextMeshProUGUI[] toCurrency_text;
    public TextMeshProUGUI[] title_toCurrency;

    public float default_cash;
    public TextMeshProUGUI exchangeCash;
    public Text textinputField;

    string text_cash;

    private void Start()
    {
        player_movement = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerMovement>();
        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerStatus>();
        centralBankStatus = GameObject.FindGameObjectWithTag("CentralBank").gameObject.GetComponent<CentralBankStatus>();
    }
    public void Update()
    {
        text_cash = checkTypeCurrency(fromCurrencys.value);
        float rate = exchangeRate.getExchangeRate(fromCurrencys.value, toCurrencys.value);
        string text = textinputField.text;
        bool result = float.TryParse(text, out float number);
        if (result)
        {
            if (textinputField.text == "")
            {
                text = "0";
            }
            float exCash = float.Parse(text) * rate;
            exchangeCash.text = exCash.ToString();
        }

        int len = 0;
        foreach (TextMeshProUGUI from in fromCurrency_text)
        {
            title_fromCurrency[len].text = text_cash;
            from.text = default_cash.ToString();
            len += 1;
        }

        int n = 0;
        for(int i = 0; i < 6; i++)
        {
            if (fromCurrencys.value == i)
            {
                continue;
            }
            float rate2 = exchangeRate.getExchangeRate(fromCurrencys.value, i);
            toCurrency_text[n].text = (default_cash * rate2).ToString("F");
            title_toCurrency[n].text = checkTypeCurrency(i);
            n++;
        }

    }

    string checkTypeCurrency(int currency_index)
    {
        if (currency_index == 0)
        {
            return "Section1";
        }
        else if (currency_index == 1)
        {
            return "Section2";
        }
        else if (currency_index == 2)
        {
            return "Section3";
        }
        else if (currency_index == 3)
        {
            return "Section4";
        }
        else if (currency_index == 4)
        {
            return "Section5";
        }
        else
        {
            return "Gold";
        }
    }

    public void show()
    {
        gameObject.SetActive(true);
        player_movement.isLooking = true;
    }

    public void hide()
    {
        gameObject.SetActive(false);
        player_movement.isLooking = false;
    }
}
