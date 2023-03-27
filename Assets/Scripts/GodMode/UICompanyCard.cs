using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Permissions;

public class UICompanyCard : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI company_name;
    public TextMeshProUGUI company_address;
    public TextMeshProUGUI balance;
    public TextMeshProUGUI dept;

    public void SetData(Sprite logo, string name, string address, float balance, float dept)
    {
        image.sprite = logo;
        company_name.text = name;
        company_address.text = address;
        this.balance.text = $"balance: {balance}";
        this.dept.text = $"dept: {dept}";
    }
}
