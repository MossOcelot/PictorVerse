using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using static UnityEngine.PlayerLoop.PreLateUpdate;

public class UICompanyController : MonoBehaviour
{
    public CompanyDB companyDB;
    [SerializeField]
    private Transform CompanyTable;
    [SerializeField]
    private GameObject CompanyCardTempalte;

    GameObject CompanyCard;

    int updateTime = 10;

    public int time;
    int nextUpdate;

    private void Start()
    {
        foreach (CompanyDB.CompanyDataTemplate data in companyDB.ImportCompanyDB())
        {
            CompanyCard = Instantiate(CompanyCardTempalte, CompanyTable);
            CompanyCard.gameObject.GetComponent<UICompanyCard>().SetData(data.logo, data.name, data.address, data.balance, data.dept);
        }
    }
    private void Update()
    {
        if (Time.time >= nextUpdate)
        {
            // Change the next update (current second+1)
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            time += 1;
            if (time >= updateTime)
            {
                int len = CompanyTable.childCount;
                if (len > 0)
                {
                    for (int i = 0; i < len; i++)
                    {
                        Destroy(CompanyTable.GetChild(i).gameObject);
                    }
                }
                foreach(CompanyDB.CompanyDataTemplate data in companyDB.ImportCompanyDB())
                {
                    CompanyCard = Instantiate(CompanyCardTempalte, CompanyTable);
                    CompanyCard.gameObject.GetComponent<UICompanyCard>().SetData(data.logo, data.name, data.address, data.balance, data.dept);
                }
                time = 0;
            }
        }
    }
}
