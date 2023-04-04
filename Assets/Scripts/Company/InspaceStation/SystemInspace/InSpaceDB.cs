using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSpaceDB : MonoBehaviour
{
    [SerializeField]
    private List<ArriveToLeaveData> arriveToLeaveDB;

    public List<ArriveToLeaveData> GetArriveToLeaveDB()
    {
        return arriveToLeaveDB;
    }

    public ArriveToLeaveData GetArriveToLeaveData(string ArriveStation, string LeaveStation)
    {
        foreach(ArriveToLeaveData data in arriveToLeaveDB)
        {
            if(data.ArriveStation == ArriveStation && data.LeaveStation == LeaveStation)
            {
                return data;
            }
        }

        return null;
    }
}

[System.Serializable]
public class ArriveToLeaveData
{
    public string ArriveStation;
    public string LeaveStation;
    public float distance;
    public float price;
    public int stamina;
}