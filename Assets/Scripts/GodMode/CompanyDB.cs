using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyDB : MonoBehaviour
{
    [System.Serializable]
    public class CompanyDataTemplate
    {
        public string id;
        public Sprite logo;
        public string name;
        public string address;
        public float balance;
        public float dept;
    }

    [SerializeField] 
    private List<CompanyDataTemplate> companyDB;

    public List<CompanyDataTemplate> ImportCompanyDB()
    {
        return companyDB;
    }

    public void AddData(CompanyDataTemplate data)
    {
        companyDB.Add(data);
    }

    public bool UpdateData(string id,  CompanyDataTemplate data)
    {
        int len = companyDB.Count;
        for(int i = 0; i < len; i++)
        {
            CompanyDataTemplate comp = companyDB[i];
            if (comp.id == id)
            {
                companyDB[i] = data;
                return true;
            }
        }
        return false;
    }

    public bool DeleteData(string id)
    {
        int len = companyDB.Count;
        for (int i = 0; i < len; i++)
        {
            CompanyDataTemplate comp = companyDB[i];
            if (comp.id == id)
            {
                companyDB.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

}
