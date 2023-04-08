using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestAcceptanceConditions.CareerModel;

[CreateAssetMenu(fileName = "New Conditions", menuName = "Mission/QuestAcceptanceConditions")]
public class QuestAcceptanceConditions : ScriptableObject
{
    public enum QuestAcceptanceType { Career, money };
      
    public QuestAcceptanceType type;

    [System.Serializable] 
    public class CareerModel
    {
        public enum CareerModelType { none, be };
        public CareerModelType type;
        public string careerName;
    }

    public CareerModel career_data;
    [TextArea]
    public string description;

}
