using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInsuranceData
{
    public InsuranceItems player_endowment;
    public InsuranceItems player_hearth_insurance;

    public PlayerInsuranceData (InsuranceController player_insurance)
    {
        player_endowment = player_insurance.GetPlayer_endowment();
        player_hearth_insurance = player_insurance.GetPlayer_hearth_insurance();
    }
}
