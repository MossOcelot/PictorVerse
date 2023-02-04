using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GovermentPolicy : MonoBehaviour
{
    [SerializeField]
    private GovermentStatus govermentStatus;

    [SerializeField]
    private int vat_tax;
    [SerializeField]
    private int business_tax;
    [SerializeField]
    private int individual_tax;
    [SerializeField]
    private int travel_tax;
    [SerializeField]
    private int vehicle_tax;
    [SerializeField]
    private int personal_star_tax;

    public int getVat()
    {
        return vat_tax;
    }
    // Start is called before the first frame update
    void Awake()
    {
        Dictionary<string, dynamic> vat_check = new VATInGalaxy().checkvat(govermentStatus.govermentInSection.ToString());
        
        vat_tax = vat_check["vat_tax"];
        business_tax = vat_check["business_tax"];
        travel_tax = vat_check["travel_tax"];
        vehicle_tax = vat_check["vehicle_tax"];
        personal_star_tax = vat_check["personal_star_tax"];
    }

    
}
