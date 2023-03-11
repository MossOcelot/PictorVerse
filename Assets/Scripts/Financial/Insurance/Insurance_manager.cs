using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insurance_manager : MonoBehaviour
{
    public GameObject Banker;
    public string insurance_seller;
    [SerializeField]
    private List<InsuranceItems> endowments;
    [SerializeField]
    private List<InsuranceItems> health_insurances;

    public void SetEndowments(List<InsuranceItems> newEndowments)
    {
        this.endowments = newEndowments;
    }

    public void SetHealth_insurances(List<InsuranceItems> newHealth_insurances)
    {
        this.health_insurances = newHealth_insurances;
    }

    public List<InsuranceItems> GetEndowments()
    {
        return endowments;
    }

    public List<InsuranceItems> GetHealthInsurance()
    {
        return health_insurances;
    }

    public void SetInsuranceData(string insuranceName)
    {
        this.insurance_seller = insuranceName;
    }

    public void Close()
    {
        Banker.gameObject.GetComponent<NPCController>().playerIsClose = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
