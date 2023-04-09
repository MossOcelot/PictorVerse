using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CareerSO", menuName = "Career/CareerSO")]
public class CareerSO : ScriptableObject
{
    public string career_name;
    public float salary_default;
    public float salary;
    public int AmountDailyQuest;
    public SceneStatus.section section_workplace;
}
