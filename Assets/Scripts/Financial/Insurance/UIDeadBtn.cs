using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDeadBtn : MonoBehaviour
{
    public GameObject Page2;
    public GameObject Page1;

    
    public void Confirm()
    {
        Debug.Log("Hello");
        Page1.SetActive(false);
        Page2.SetActive(true);
        UIPage2Controller page2Controller = Page2.GetComponent<UIPage2Controller>();
        page2Controller.AddAllItems();

        InsuranceController insuranceController = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<InsuranceController>();
        page2Controller.SetInsuranceSO(insuranceController.GetPlayer_endowment());

        bool status = insuranceController.DeadClearItem();

        page2Controller.AddAllInsuranceItems();

        page2Controller.AddInsuranceDetailsObj(!status);
    }
}
