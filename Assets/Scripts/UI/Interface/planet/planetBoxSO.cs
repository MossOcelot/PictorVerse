using inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu]
public class planetBoxSO : ScriptableObject
{
    [SerializeField]
    private List<planetItem> planetItems;

    [field: SerializeField]
    public int Size { get; private set; } = 2;

    public event Action<Dictionary<int, planetItem>> OnplanetUpdated;
    public void Initialize()
    {
        planetItems = new List<planetItem>();
        for (int i = 0; i < Size; i++)
        {
            planetItems.Add(planetItem.GetEmptyItem());
        }
    }

    public void AddItem(planetItem item)
    {
        AddItem(item.item, item.owner, item.type);
    }

    public void AddItem(planetSO item, string owner, string type)
    {
       // Debug.Log("cout" + planetItems.Count);
        for (int i = 0; i < planetItems.Count; i++)
        {
            if (planetItems[i].IsEmpty)
            {
                planetItems[i] = new planetItem
                {
                    item = item,
                    owner = owner,
                    type = type,
                };
                return;
            }
        }
    }


    public Dictionary<int, planetItem> GetCurrentPlanetState()
    {
        Dictionary<int, planetItem> returnValue =
            new Dictionary<int, planetItem>();

        for (int i = 0; i < planetItems.Count; i++)
        {
            if (planetItems[i].IsEmpty)
            continue;

            returnValue[i] = planetItems[i];

            
        }
        return returnValue;
       
    }

    public planetItem GetItemAt(int itemIndex)
    {
        OnplanetUpdated?.Invoke(GetCurrentPlanetState());
        return planetItems[itemIndex];
    }
}


[Serializable]
public struct planetItem
{
    public planetSO item;
    public string owner;
    public string type;
    public bool IsEmpty => item == null;

    public planetItem ChangeResourceData(InventorySO newResourceData)
    {
        return new planetItem
        {
            item = this.item,
            owner = this.owner,
            type = this.type,

        };
    }
    public planetItem ChangeOwner(string newOwner)
    {
        return new planetItem
        {
            item = this.item,
            owner = newOwner,
            type = this.type,

        };
    }

    public planetItem ChangeType(string newType)
    {
        return new planetItem
        {
            item = this.item,
            owner = this.owner,
            type = newType,
        };
    }

    public static planetItem GetEmptyItem()
        => new planetItem
        {
            item = null,
            owner = null,
            type = null
        };
}