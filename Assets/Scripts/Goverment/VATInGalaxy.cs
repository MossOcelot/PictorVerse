using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VATInGalaxy : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<string, dynamic> checkvat(string section)
    {
        switch(section)
        {
            case "section1":
                return new Dictionary<string, dynamic>
                {
                    {"vat_tax",0 },
                    {"business_tax", 5 },
                    {"travel_tax", 20},
                    {"vehicle_tax", 200},
                    {"personal_star_tax", 0}
                };
            case "section2":
                return new Dictionary<string, dynamic>
                {
                    {"vat_tax",25 },
                    {"business_tax", 20 },
                    {"travel_tax", 10},
                    {"vehicle_tax", 40},
                    {"personal_star_tax", 10}
                };
            case "section3":
                return new Dictionary<string, dynamic>
                {
                    {"vat_tax",10 },
                    {"business_tax", 20 },
                    {"travel_tax", 10},
                    {"vehicle_tax", 40},
                    {"personal_star_tax", 10}
                };
            case "section4":
                return new Dictionary<string, dynamic>
                {
                    {"vat_tax",10 },
                    {"business_tax", 20 },
                    {"travel_tax", 10},
                    {"vehicle_tax", 40},
                    {"personal_star_tax", 10}
                };
            case "section5":
                return new Dictionary<string, dynamic>
                {
                    {"vat_tax",10 },
                    {"business_tax", 20 },
                    {"travel_tax", 10},
                    {"vehicle_tax", 40},
                    {"personal_star_tax", 10}
                };
            default: return new Dictionary<string, dynamic>
                {
                    {"vat_tax",7 },
                    {"business_tax", 20 },
                    {"travel_tax", 10},
                    {"vehicle_tax", 40},
                    {"personal_star_tax", 10}
                };
        }
    }

}
