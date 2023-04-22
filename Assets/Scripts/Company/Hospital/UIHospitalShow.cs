using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIHospitalShow : MonoBehaviour
{
    public TextMeshProUGUI Head_name;
    public TextMeshProUGUI Disease_name;
    public TextMeshProUGUI cost_text;

    private StatusEffectController disease_status;

    public UIHealing uiHealing;
    [SerializeField]
    private HospitalDB DB;
    private void Start()
    {
        disease_status = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<StatusEffectController>();
    }

    private void FixedUpdate()
    {
        List<StatusEffectPlayer> diseases = disease_status.GetstatusEffects();
        string d_name = "";
        float cost = 0;
        if(diseases.Count == 0)
        {
            Head_name.text = "คุณไม่ป่วย";
            Disease_name.text = $"โรค: ไม่มี";
        }
        else
        {
            Head_name.text = "คุณป่วย!";
        }

        foreach(StatusEffectPlayer disease in diseases)
        {
            cost += DB.GetCost(disease.data);
            d_name += $"{disease.data.effect_name} ";
        }

        Disease_name.text = $"โรค: {d_name}";
        cost_text.text = $"ค่ารักษา : {cost} <sprite index={(int)DB.section}>";
        
        if(uiHealing.isActiveAndEnabled)
        {
            uiHealing.SetData(d_name, cost);
        }
    }

    public void Show()
    {
        Debug.Log("Show");
        GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<interfaceCanvasController>().isLooking = true;
    }
    public void Close()
    {
        GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<interfaceCanvasController>().isLooking = false;
        Destroy(gameObject);
    }
    
}
