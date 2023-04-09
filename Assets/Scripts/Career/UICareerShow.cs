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
            career_text.text = $"อาชีพ : {PlayerCareer.career_name}";
            career_salary.text = $"เงินเดือน : {PlayerCareer.salary} <sprite index={(int)PlayerCareer.section_workplace}>";
        } else
        {
            career_text.text = "อาชีพ : ไม่มี";
            career_salary.text = "เงินเดือน : ไม่มี";
        }
    }
}
