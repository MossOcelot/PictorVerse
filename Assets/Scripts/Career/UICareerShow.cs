using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UICareerShow : MonoBehaviour
{
    public TextMeshProUGUI career_text;
    public TextMeshProUGUI career_salary;

    private CareerPlayer career;

    private void Start()
    {
        career = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CareerPlayer>();    
    }
    // Update is called once per frame
    private void Update()
    {
        CareerSO PlayerCareer = career.Career;

        if (PlayerCareer != null)
        {
            career_text.text = $"�Ҫվ : {PlayerCareer.career_name}";
            career_salary.text = $"�Թ��͹ : {PlayerCareer.salary} <sprite index={(int)PlayerCareer.section_workplace}>";
        } else
        {
            career_text.text = "�Ҫվ : �����";
            career_salary.text = "�Թ��͹ : �����";
        }
    }
}
