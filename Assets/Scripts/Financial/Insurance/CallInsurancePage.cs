using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallInsurancePage : MonoBehaviour
{
    [SerializeField]
    private List<InsuranceItems> endowments;
    [SerializeField]
    private List<InsuranceItems> health_insurances;

    [SerializeField]
    private Insurance_manager insurance_manager;
    private void Start()
    {
        insurance_manager = GameObject.FindGameObjectWithTag("Insurance").gameObject.GetComponent<Insurance_manager>();
    }

    public void SetInsurance()
    {
        insurance_manager.SetEndowments(endowments);
        insurance_manager.SetHealth_insurances(health_insurances);
    }

    public List<InsuranceItems> GetEndowments()
    {
        return endowments;
    }

    public void LoadEndowments(List<InsuranceItems> dataendowments)
    {
        this.endowments = dataendowments;
    }

    public List<InsuranceItems> GetHealth_insurances()
    {
        return health_insurances;
    }

    public void LoadHealth_insurances(List<InsuranceItems> datahealth_insurances)
    {
        this.health_insurances = datahealth_insurances;
    }


}
