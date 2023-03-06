using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace inventory.Model
{
    [CreateAssetMenu]
    public class EatableItem : Item, IDestroyableItem, IItemAction, IUSEAction
    {
        [SerializeField]
        private List<ModifierData> modifierDatas = new List<ModifierData>();
        public string ActionName => "Consume";
        public string NoActionName => null;

        // public AudioClip actionSFX => {get; private set;}
        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            foreach (ModifierData data in modifierDatas)
            {
                data.statModifier.AffectCharacter(character, data.value);
            }
            return true;
        }

        public bool NoperformAction(GameObject character, List<ItemParameter> itemState)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IDestroyableItem
    {

    }

    public interface IItemAction
    {
        public string ActionName { get; }
        public string NoActionName { get; }
        //public AudioClip actionSFX { get; }
        bool PerformAction(GameObject character, List<ItemParameter> itemState);
        bool NoperformAction(GameObject character, List<ItemParameter> itemState);
    }

    public interface IUSEAction
    {
        bool UseAction(GameObject character, int quantity, List<ItemParameter> itemState);
        bool NotUseAction(GameObject character, int quantity, List<ItemParameter> itemState);
    }



    [System.Serializable]
    public class ModifierData
    {
        public CharacterStatModifierSO statModifier;
        public float value;
    }
}