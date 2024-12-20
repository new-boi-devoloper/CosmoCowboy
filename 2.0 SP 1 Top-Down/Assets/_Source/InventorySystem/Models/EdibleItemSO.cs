using System;
using System.Collections.Generic;
using InventorySystem.Models.StatModifiers;
using UnityEngine;

namespace InventorySystem.Models
{
    [CreateAssetMenu(fileName = "Edible Item", menuName = "ItemSO/Edible Item")]
    public class EdibleItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public List<ModifierData> modifiersData = new();

        public string ActionName => "Consume";
        public AudioClip ActionSound { get; }

        public static event Action<int> OnAddHPToPlayer;
        public bool PerformAction(GameObject weaponPlace, List<ItemParameter> itemState = null)
        {
            foreach (var data in modifiersData)
            {
                OnAddHPToPlayer?.Invoke(1);
                data.statModifier.AffectCharacter(weaponPlace, data.value);
                return true;
            }

            return false;
        }
    }

    public interface IDestroyableItem
    {
    }

    public interface IItemAction
    {
        public string ActionName { get; }
        public AudioClip ActionSound { get; }
        bool PerformAction(GameObject weaponPlace, List<ItemParameter> itemState);
    }

    [Serializable]
    public class ModifierData
    {
        public CharacterStatModifierSO statModifier;
        public int value;
    }
}