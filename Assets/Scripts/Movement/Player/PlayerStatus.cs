using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    private string playername;
    [SerializeField]
    private int HP;
    [SerializeField]
    private int energy;
    public int static_useEnergy;
    public void setEnergy(int useEnergy)
    {
        this.energy += useEnergy;
    }

    public int getEnergy()
    {
        return this.energy;
    }

    
    private void Update()
    {
        if (this.energy <= 0) {
            Debug.Log("Empty Energy");
            this.energy = 0;
        }
        if (this.HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
