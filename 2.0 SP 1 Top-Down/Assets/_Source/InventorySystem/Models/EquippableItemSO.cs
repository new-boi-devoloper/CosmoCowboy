using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

namespace InventorySystem.Models
{
    [CreateAssetMenu(fileName = "Equippable Item", menuName = "ItemSO/Equippable Item")]
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public GameObject weaponPrefab; // Добавляем поле для хранения префаба оружия
        public string ActionName => "Equip";
        public AudioClip ActionSound { get; }

        public bool PerformAction(GameObject weaponPlace, List<ItemParameter> itemState = null)
        {
            var weaponSystem = weaponPlace.GetComponent<AgentWeapon>();
            if (weaponSystem != null)
            {
                weaponSystem.SetWeapon(this, itemState ?? DefaultParametersList);
                return true;
            }

            return false;
        }
    }
}