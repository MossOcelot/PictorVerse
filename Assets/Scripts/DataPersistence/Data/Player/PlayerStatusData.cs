using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatusData
{
    public string player_name;
    public PocketDetails player_accounts;
    public Financial_Details financial_detail;
    public List<AccountsDetail> accountsDetails;
    public PlayerStatus.StaticValue myStatic;
    public string account_id;
    
    public int MaxHP;
    public int HP;
    public int MaxEnergy;
    public int Energy;
    public bool IsDead;

    public float[] position_player;

    public PlayerStatusData(PlayerStatus player_status)
    {
        player_name = player_status.getPlayerName();
        player_accounts = player_status.player_accounts;
        financial_detail = player_status.getFinancial_Details();
        accountsDetails = player_status.getAccountsDetails();
        myStatic = player_status.getMyStaticFromData();
        account_id = player_status.GetAccountID();
        MaxHP = player_status.getMaxHP();
        HP = player_status.getHP();
        MaxEnergy = player_status.getMaxEnergy();
        Energy = player_status.getEnergy();
        IsDead = player_status.GetIsDead();

        position_player = new float[3];
        position_player[0] = player_status.transform.position.x;
        position_player[1] = player_status.transform.position.y;
        position_player[2] = 0;
    }


}
