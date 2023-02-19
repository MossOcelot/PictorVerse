using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace inventory.Model
{
    [CreateAssetMenu]
    public class EquippableItem : Item, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";

        // public AudioClip actionSFX { get; private set; }

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();

            if(weaponSystem != null)
            {
                weaponSystem.SetWeapon(this, itemState == null ? 
                    DefaultParametersList : itemState);
                // ����ŧ WeaponBox
                return true;
            }
            return false;
        }
    }
}
