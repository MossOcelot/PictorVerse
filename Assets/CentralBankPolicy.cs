using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralBankPolicy : MonoBehaviour
{
    [SerializeField]
    private CentralBankStatus centralBankStatus;

    [SerializeField]
    private float policyInterestRate;

    public float GetPolicyInterestRate()
    {
        return this.policyInterestRate;
    }
}
