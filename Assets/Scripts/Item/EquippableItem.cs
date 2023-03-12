using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace inventory.Model
{
    [CreateAssetMenu]
    public class EquippableItem : Item, IDestroyableItem, IUSEAction, IItemAction
    {
        public string ActionName => "Equip";
        public string NoActionName => "Remove";

        // public AudioClip actionSFX { get; private set; }

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();

            if(weaponSystem != null)
            {
                weaponSystem.SetWeapon(this, itemState == null ? 
                    DefaultParametersList : itemState);
                return true;
            }
            return false;
        }

        public bool NoperformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
            if (weaponSystem != null)
            {
                weaponSystem.RemoveWeapon(this, itemState == null ?
                    DefaultParametersList : itemState);
                return true;
            }
            return false;

        }

    }
}
