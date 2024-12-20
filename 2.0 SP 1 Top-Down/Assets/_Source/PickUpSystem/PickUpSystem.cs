using System;
using System.Collections.Generic;
using InventorySystem.Models;
using UnityEngine;

namespace PickUpSystem
{
    public class PickUpSystem : MonoBehaviour
    {
        [SerializeField] private InventorySO inventoryData;
        [SerializeField] private List<LootDropConfig> lootTable; // Список конфигураций для выпадения предметов
        [SerializeField] private float dropChance = 0.5f; // Шанс выпадения предмета

        [Header("Tutor Info")] [SerializeField]
        private int index = 2;

        private bool _oneTimeTutor = true;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OneTimeSolutionForTutor(); 
            var item = collision.GetComponent<Item>();
            if (item != null)
            {
                var reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
                if (reminder == 0)
                    item.DestroyItem();
                else
                    item.Quantity = reminder;
            }
        }

        // private void OnEnable()
        // {
        //     HumanoidEnemy.OnItemToPlace += HandleEnemyDeath;
        // }
        //
        // private void OnDisable()
        // {
        //     HumanoidEnemy.OnItemToPlace -= HandleEnemyDeath;
        // }
        //
        // private void HandleEnemyDeath(Vector3 deathPosition)
        // {
        //     if (Random.value > dropChance)
        //     {
        //         Debug.Log("Item drop chance failed.");
        //         return;
        //     }
        //
        //     if (lootTable == null || lootTable.Count == 0)
        //     {
        //         Debug.LogWarning("No loot table specified in PickUpSystem.");
        //         return;
        //     }
        //
        //     var lootConfig = lootTable[Random.Range(0, lootTable.Count)];
        //     var quantity = Random.Range((int)lootConfig.MinQuantity, lootConfig.MaxQuantity + 1); // Рандомное количество
        //
        //     Debug.Log($"Spawning item: {lootConfig.Item.InventoryItem.Name} (Quantity: {quantity}) at {deathPosition}");
        //
        //     var itemObject = Instantiate(lootConfig.Item.InventoryItem, deathPosition, Quaternion.identity);
        //
        //     var itemComponent = itemObject.GetComponent<Item>();
        //     if (itemComponent != null)
        //     {
        //         itemComponent.InventoryItem = lootConfig.Item.InventoryItem;
        //         itemComponent.Quantity = quantity;
        //     }
        // }

        public static event Action<int> OnTutorRequest;

        private void OneTimeSolutionForTutor()
        {
            if (_oneTimeTutor)
            {
                OnTutorRequest?.Invoke(index);
                _oneTimeTutor = false;
            }
        }
    }

    [Serializable]
    public class LootDropConfig
    {
        [field: SerializeField] public Item Item { get; private set; } // Предмет для выпадения
        [field: SerializeField] public int MinQuantity { get; private set; } // Минимальное количество
        [field: SerializeField] public int MaxQuantity { get; private set; } // Максимальное количество
    }
}