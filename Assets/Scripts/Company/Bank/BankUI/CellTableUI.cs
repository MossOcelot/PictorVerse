using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CellTableUI : MonoBehaviour
{
    public TextMeshProUGUI Date_text;
    public TextMeshProUGUI list_name;
    public TextMeshProUGUI desposit_text;
    public TextMeshProUGUI withdraw_text;
    public TextMeshProUGUI number_text;

    public void SetData(int[] date, string name, float desposit, float withdraw, string number)
    {
        Date_text.text = $"{date[0]}/{date[1]}/{date[2]} {date[3]}:{date[4]}";
        list_name.text = name;
        desposit_text.text = desposit.ToString();
        withdraw_text.text = withdraw.ToString();
        number_text.text = number;
    }
}
