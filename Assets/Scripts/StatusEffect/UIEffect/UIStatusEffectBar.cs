using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatusEffectBar : MonoBehaviour
{
    [SerializeField]
    private UIStatusEffectItem statusEffect_template;
    [SerializeField]
    private GameObject description;

    [SerializeField]
    private Transform statusEffect_content;

    List<UIStatusEffectItem> listOfUIstatusEffect = new List<UIStatusEffectItem>();

    public event Action<int> OnDescriptionRequested, OnHideDescriptionRequested;
    public List<GameObject> objs = new List<GameObject>();

    public GameObject description_content;
    public void InitializeUIStatusEffect(int index)
    {
        ClearUI();
        for(int i = 0; i < index; i++)
        {
            UIStatusEffectItem uiItem = Instantiate(statusEffect_template, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(statusEffect_content);
            listOfUIstatusEffect.Add(uiItem);

            uiItem.OnEnterMouseBtn += HandleShowItemDescription;
            uiItem.OnExitMouseBtn += HandleHideItemDescription;
        }
    }
    public void UpdateData(int itemIndex, StatusEffectPlayer status_data)
    {
        if (listOfUIstatusEffect.Count > itemIndex)
        {
            listOfUIstatusEffect[itemIndex].SetData(status_data);
        }
    }

    private void HandleShowItemDescription(UIStatusEffectItem statusEffect)
    {
        int index = listOfUIstatusEffect.IndexOf(statusEffect);
        if (index == -1)
        {
            return;
        }
        OnDescriptionRequested?.Invoke(index);
    }

    private void HandleHideItemDescription(UIStatusEffectItem statusEffect)
    {
        int index = listOfUIstatusEffect.IndexOf(statusEffect);
        if (index == -1)
        {
            return;
        }
        OnHideDescriptionRequested?.Invoke(index);
    }

    public void AddDescription(StatusEffectPlayer data)
    {
        int len = description_content.transform.childCount;
        if(len > 0)
        {
            for(int i = 0; i < len; i++)
            {
                Destroy(description_content.transform.GetChild(i).gameObject);
            }
        }
        description_content.gameObject.GetComponent<DescriptionTemplate>().AddDescription(data);
    }

    public void showItemDescriptionAction(int itemIndex)
    {
        description_content.transform.position = listOfUIstatusEffect[itemIndex].transform.position + new Vector3(100.002f, 239.422f);
    }
    public void AllDestroyDescription()
    {
        int len = description_content.transform.childCount;
        if (len > 0)
        {
            for (int i = 0; i < len; i++)
            {
                Destroy(description_content.transform.GetChild(i).gameObject);
            }
        }
    }
    private void ClearUI()
    {
        foreach(UIStatusEffectItem item in listOfUIstatusEffect)
        {
            item.ResetData();
            Destroy(item.gameObject);
        }
        listOfUIstatusEffect.Clear();
    }

}

[System.Serializable]
public class StatusEffectPlayer
{
    public StatusEffectData data;
    public int current_time = 0;
    public bool StatusSlow;
    public StatusEffectPlayer(StatusEffectData data)
    {
        this.data = data;
    }
}