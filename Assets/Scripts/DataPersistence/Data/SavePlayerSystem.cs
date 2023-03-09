using UnityEngine;
using System.IO;
public static class SavePlayerSystem 
{
    public static void SavePlayerStatus (PlayerStatus player_status)
    {
        PlayerStatusData savePlayerStatus = new PlayerStatusData(player_status);

        string path = Application.dataPath + "/data/player";
        string json = JsonUtility.ToJson(savePlayerStatus);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/PlayerStatus.json", json);
    }

    public static PlayerStatusData LoadPlayerStatus()
    {
        string path = Application.dataPath + "/data/player/PlayerStatus.json";

        if(File.Exists(path))
        {
            string saveString = File.ReadAllText(path);
            PlayerStatusData savePlayerStatus = JsonUtility.FromJson<PlayerStatusData>(saveString);

            return savePlayerStatus;
        } else
        {
            Debug.LogError("Save file not found in => " + path);
            return null;
        }
    }

    public static void SavePlayerMovement (PlayerMovement player_movement)
    {
        PlayerMovementData savePlayerMovement = new PlayerMovementData(player_movement);

        string path = Application.dataPath + "/data/player";
        string json = JsonUtility.ToJson(savePlayerMovement);
        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/PlayerMovement.json", json);
    }

    public static PlayerMovementData LoadPlayerMovement()
    {
        string path = Application.dataPath + "/data/player/PlayerMovement.json";

        if (File.Exists(path))
        {
            string saveString = File.ReadAllText(path);
            PlayerMovementData savePlayermovement = JsonUtility.FromJson<PlayerMovementData>(saveString);

            return savePlayermovement;
        }
        else
        {
            Debug.LogError("Save file not found in => " + path);
            return null;
        }
    }

    public static void SavePlayerInventory(InventoryController player_inventory)
    {
        PlayerInventoryData savePlayerInventory = new PlayerInventoryData(player_inventory);

        string path = Application.dataPath + "/data/player";
        string json = JsonUtility.ToJson(savePlayerInventory);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/PlayerInventory.json", json);
    }

    public static PlayerInventoryData LoadPlayerInventory()
    {
        string path = Application.dataPath + "/data/player/PlayerInventory.json";

        if (File.Exists(path))
        {
            string saveString = File.ReadAllText(path);
            PlayerInventoryData savePlayerInventory = JsonUtility.FromJson<PlayerInventoryData>(saveString);

            return savePlayerInventory;
        }
        else
        {
            Debug.LogError("Save file not found in => " + path);
            return null;
        }
    }

    public static void SavePlayerInsurance(InsuranceController player_insurance)
    {
        PlayerInsuranceData savePlayerInsurance = new PlayerInsuranceData(player_insurance);

        string path = Application.dataPath + "/data/player";
        string json = JsonUtility.ToJson(savePlayerInsurance);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/PlayerInsurance.json", json);
    }

    public static PlayerInsuranceData LoadPlayerInsurance()
    {
        string path = Application.dataPath + "/data/player/PlayerInsurance.json";

        if (File.Exists(path))
        {
            string saveString = File.ReadAllText(path);
            PlayerInsuranceData savePlayerInsurance = JsonUtility.FromJson<PlayerInsuranceData>(saveString);

            return savePlayerInsurance;
        }
        else
        {
            Debug.LogError("Save file not found in => " + path);
            return null;
        }
    }

    public static void SavePlayerEquipment(UI_characterEquipment player_equipment)
    {
        PlayerEquipmentData savePlayerEquipment = new PlayerEquipmentData(player_equipment);

        string path = Application.dataPath + "/data/player";
        string json = JsonUtility.ToJson(savePlayerEquipment);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/PlayerEquipment.json", json);
    }

    public static PlayerEquipmentData LoadPlayerEquipment()
    {
        string path = Application.dataPath + "/data/player/PlayerEquipment.json";

        if (File.Exists(path))
        {
            string saveString = File.ReadAllText(path);
            PlayerEquipmentData savePlayerEquipment = JsonUtility.FromJson<PlayerEquipmentData>(saveString);

            return savePlayerEquipment;
        }
        else
        {
            Debug.LogError("Save file not found in => " + path);
            return null;
        }
    }

    public static void SavePlayerAgentWeapon(AgentWeapon agentWeapon)
    {
        PlayerAgentWeaponData savePlayerAgentWeapon = new PlayerAgentWeaponData(agentWeapon);

        string path = Application.dataPath + "/data/player";
        string json = JsonUtility.ToJson(savePlayerAgentWeapon);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/PlayerAgentWeapon.json", json);
    }

    public static PlayerAgentWeaponData LoadPlayerAgentWeapon()
    {
        string path = Application.dataPath + "/data/player/PlayerAgentWeapon.json";

        if (File.Exists(path))
        {
            string saveString = File.ReadAllText(path);
            PlayerAgentWeaponData savePlayerAgentWeapon = JsonUtility.FromJson<PlayerAgentWeaponData>(saveString);

            return savePlayerAgentWeapon;
        }
        else
        {
            Debug.LogError("Save file not found in => " + path);
            return null;
        }
    }

    public static void SavePlayerAgentSet(AgentSet agentSet)
    {
        PlayerAgentSetData savePlayerAgentSet = new PlayerAgentSetData(agentSet);

        string path = Application.dataPath + "/data/player";
        string json = JsonUtility.ToJson(savePlayerAgentSet);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/PlayerAgentSet.json", json);
    }

    public static PlayerAgentSetData LoadPlayerAgentSet()
    {
        string path = Application.dataPath + "/data/player/PlayerAgentSet.json";

        if (File.Exists(path))
        {
            string saveString = File.ReadAllText(path);
            PlayerAgentSetData savePlayerAgentSet = JsonUtility.FromJson<PlayerAgentSetData>(saveString);

            return savePlayerAgentSet;
        }
        else
        {
            Debug.LogError("Save file not found in => " + path);
            return null;
        }
    }


}


