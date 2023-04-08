using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIGoverment : MonoBehaviour
{
    public GodModeManager godmode;
    public enum section { section1, section2, section3, section4, section5 }
    public section GovermentInSection;
    public TextMeshProUGUI GovermentName;
    public TextMeshProUGUI[] Taxs;
    public TextMeshProUGUI CollectTaxValue;

    GovermentPolicy policy;

    string oldsection = "";
    
    private void Start()
    {
        policy = GameObject.FindGameObjectWithTag("Goverment").gameObject.GetComponent<GovermentPolicy>();
    }

    private void Update()
    {
        SceneStatus.section section = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection;

        if ((oldsection != section.ToString() && section.ToString() == GovermentInSection.ToString()) || godmode.reset)
        {
            GovermentName.text = policy.govermentStatus.goverment.name_goverment.ToString();
            Taxs[0].text = $"{policy.govermentPolicy.individual_tax.Last().Tax} %";
            Taxs[1].text = $"{policy.govermentPolicy.business_tax} %";
            Taxs[2].text = $"{policy.govermentPolicy.vat_tax} %";
            CollectTaxValue.text = $"<sprite index={(int)policy.govermentStatus.goverment.govermentInSection}> {policy.govermentStatus.GetTaxIncome()}";
            oldsection = section.ToString();
            godmode.reset = false;
        }
        
    }
}
