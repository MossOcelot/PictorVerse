using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CellTableUI : MonoBehaviour
{
    public Text Date_text;
    public Text list_name;
    public Text desposit_text;
    public Text withdraw_text;
    public Text number_text;

    public void SetData(int[] date, string name, float desposit, float withdraw, string number)
    {
        Date_text.text = $"{date[0]}/{date[1]}/{date[2]} {date[3]}:{date[4]}:{date[5]}";
        list_name.text = name;
        desposit_text.text = desposit.ToString();
        withdraw_text.text = withdraw.ToString();
        number_text.text = number;
    }
}
