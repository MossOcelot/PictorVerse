using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New HospitalDB", menuName = "Company/HospitalDB")]
public class HospitalDB : ScriptableObject
{
    public string hospital_name;
    public List<DiseaseCost> diseaseCost;
    public SceneStatus.section section;
    public float GetCost(StatusEffectData disease)
    {
        foreach(DiseaseCost cost in diseaseCost)
        {
            if(disease.effect_name == cost.disease.effect_name)
            {
                return cost.cost;
            }
        }
        return -1;
    }
}

[System.Serializable]
public class DiseaseCost
{
    public StatusEffectData disease;
    public float cost;
}
