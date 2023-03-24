using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIExchangeGM : MonoBehaviour
{
    [SerializeField]
    private ExchangeRate exchangeRate;
    public TMP_Dropdown fromCurrencys;

    public TextMeshProUGUI FromCurrency;
    public TextMeshProUGUI[] ToCurrentNames;
    public TextMeshProUGUI[] ToCurrency;

    int cash = 100;
    string[] CurrentName = new string[] { "section1", "section2", "section3", "section4", "section5", "Gold", };
    private void Update()
    {
        FromCurrency.text = $"<sprite index={fromCurrencys}> {cash}";
        int n = 0;
        int len = ToCurrency.Length;
        for(int i = 0; i < len; i ++)
        {
            if (n == fromCurrencys.value)
            {
                n++;
            }
            float rate = exchangeRate.getExchangeRate(fromCurrencys.value, n);
            float newCash = cash * rate;
            string name = CurrentName[n];
            if(n == 5)
            {
                ToCurrentNames[i].text = $"{name}";
                ToCurrency[i].text = $"G {newCash}";
                continue;
            }
            ToCurrentNames[i].text = $"{name}";
            ToCurrency[i].text = $"<sprite index={n}> {newCash}";
            n++;
        }
    }
}
