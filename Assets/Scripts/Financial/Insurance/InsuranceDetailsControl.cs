using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InsuranceDetailsControl : MonoBehaviour
{
    public Image insurance_logo;
    public TextMeshProUGUI insurance_name;
    public TextMeshProUGUI insurance_effect;
    public UIAllItemInsuranceDetails allItemDetails;
    public UIAllItemInsuranceDetails allItemInsuranceDetails;
    public GameObject Introduction;
    public GameObject ItemNonePanel;

    public void SetallItemDetails(List<ItemInsuranceDetails> itemInsuranceDetails)
    {
        allItemDetails.SetData(itemInsuranceDetails);
    }

    public void SetallItemInsuranceDetails(List<ItemInsuranceDetails> itemInsuranceDetails)
    {
        allItemInsuranceDetails.SetData(itemInsuranceDetails);
    }

    public void SetDataNoneInsurance()
    {
        allItemInsuranceDetails.SetIntroduceInsurance();
        Introduction.SetActive(true);
    }

    public void SetItemNone()
    {
        allItemInsuranceDetails.SetIntroduceInsurance();
        ItemNonePanel.SetActive(true);
    }

    public void SetTitleInsurance(Sprite logo, string name, string effect)
    {
        insurance_logo.sprite = logo;
        insurance_name.text = name;
        insurance_effect.text = effect;
    }

    public void SetTitleInsuranceNone()
    {
        insurance_logo.enabled = false;
        insurance_name.text = "ไม่มี";
        insurance_effect.text = "ไม่มี";
    }
}
