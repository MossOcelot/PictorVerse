using UnityEngine;
using System.IO;

public static class SaveBankSystem 
{
    public static void SaveBankCompany(BankCompany bank_company)
    {

        BankData saveBankCompany = new BankData(bank_company);

        string path = Application.dataPath + "/data/BankCompany/" + bank_company.BankName;
        string json = JsonUtility.ToJson(saveBankCompany);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/BankCompany.json", json);
    }

    public static BankData LoadBankCompany(string bank_company)
    {
        string path = Application.dataPath + "/data/BankCompany/" + bank_company + "/BankCompany.json";

        if (File.Exists(path))
        {
            string saveString = File.ReadAllText(path);
            BankData saveBankCompany = JsonUtility.FromJson<BankData>(saveString);

            return saveBankCompany;
        }
        else
        {
            Debug.LogError("Save file not found in => " + path);
            return null;
        }
    }
}
