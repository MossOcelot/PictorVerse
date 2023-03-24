using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIGoverment : MonoBehaviour
{
    public TextMeshProUGUI GovermentName;
    public TextMeshProUGUI[] Taxs;
    public TextMeshProUGUI CurrentTaxValue;

    GovermentPolicy policy;
    GovermentStatus status;

    string oldsection = "";
    private void Start()
    {
        policy = GameObject.FindGameObjectWithTag("Goverment").gameObject.GetComponent<GovermentPolicy>();
    }

    private void Update()
    {
        string section = GameObject.FindGameObjectWithTag("SceneStatus").gameObject.GetComponent<SceneStatus>().sceneInsection.ToString();

        if (oldsection != section)
        {
            GovermentName.text = policy.govermentStatus.name_goverment.ToString();
            Taxs[0].text = policy.individual_tax.Last().Tax.ToString();
            oldsection = section;   
        }
        
    }
}
