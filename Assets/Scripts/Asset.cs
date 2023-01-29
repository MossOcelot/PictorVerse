using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asset : MonoBehaviour
{
    public int asset_id => GetInstanceID(); 
    public string asset_name;
    public string asset_type;
    [SerializeField]
    private int asset_value;
    public string asset_location;

    public void setAssetValue(int newValue)
    {
        this.asset_value = newValue;
    }

    public int getAssetValue()
    {
        return this.asset_value;
    }
}
