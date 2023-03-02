using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bank_Policy : MonoBehaviour
{
    private CentralBankPolicy centralBankPolicy;

    private float BeforePolicyInterestRate;
    [SerializeField]    
    private float InterestRate;

    [SerializeField]
    private Text InteresetRate_text;

    public float GetInterestRate()
    {
        return this.InterestRate;
    }
    // Start is called before the first frame update
    void Start()
    {
        centralBankPolicy = GameObject.FindGameObjectWithTag("CentralBank").gameObject.GetComponent<CentralBankPolicy>();
    }

    private void FixedUpdate()
    {
        float policyInterestRate = centralBankPolicy.GetPolicyInterestRate();

        // Random InterestRate Bank
        if(policyInterestRate != BeforePolicyInterestRate)
        {
            BeforePolicyInterestRate = policyInterestRate;
            InterestRate = policyInterestRate * Random.Range(0.9f, 1.1f);
            InteresetRate_text.text = "�͡���� " + InterestRate.ToString("0.00") + " % ��ͻ�"; 
        }
    }

}
