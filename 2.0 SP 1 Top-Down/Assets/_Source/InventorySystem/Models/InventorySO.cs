using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem.Models
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> inventoryItems;

        [field: SerializeField] public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (var i = 0; i < Size; i++) inventoryItems.Add(InventoryItem.GetEmptyItem());
        }

        public int AddItem(ItemSO item, int quantity, List<ItemParameter> itemState = null)
        {
            if (item.IsStackable == false)
                for (var i = 0; i < inventoryItems.Count; i++)
                {
                    while (quantity > 0 && IsInventoryFull() == false)
                        quantity -= AddItemToFirstFreeSlot(item, 1, itemState);

                    InformAboutChange();
                    return quantity;
                }

            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;
        }

        private int AddItemToFirstFreeSlot(ItemSO item, int quantity, List<ItemParameter> itemState = null)
        {
            var newItem = new InventoryItem
            {
                item = item,
                quantity = quantity,
                ItemState = new List<ItemParameter>(itemState ?? item.DefaultParametersList)
            };
            for (var i = 0; i < inventoryItems.Count; i++)
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }

            return 0;
        }

        private bool IsInventoryFull()
        {
            return inventoryItems.Any(item => item.IsEmpty) == false;
        }

        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (var i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                if (inventoryItems[i].item.Id == item.Id)
                {
                    var amountPossibleToTake =
                        inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                    if (quantity > amountPossibleToTake)
                    {
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }

            while (quantity > 0 && IsInventoryFull() == false)
            {
                var newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }

            return quantity;
        }

        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            var returnValue = new Dictionary<int, InventoryItem>();

            for (var i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty) continue;
                returnValue[i] = inventoryItems[i];
            }

            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public int GetItemIndex(ItemSO item)
        {
            for (var i = 0; i < inventoryItems.Count; i++)
                if (inventoryItems[i].item == item)
                    return i;
            return -1;
        }

        public void SwapItems(int itemIndex1, int itemIndex2)
        {
            (inventoryItems[itemIndex1], inventoryItems[itemIndex2]) =
                (inventoryItems[itemIndex2], inventoryItems[itemIndex1]);

            InformAboutChange();
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }

        public void RemoveItem(int itemIndex, int amount)
        {
            if (inventoryItems.Count <= itemIndex) return;

            if (inventoryItems[itemIndex].IsEmpty) return;

            var reminder = inventoryItems[itemIndex].quantity - amount;

            if (reminder <= 0)
                inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
            else
                inventoryItems[itemIndex] = inventoryItems[itemIndex]
                    .ChangeQuantity(reminder);

            InformAboutChange();
        }
    }

    [Serializable]
    public struct InventoryItem
    {
        public int quantity;

        public ItemSO item;

        public bool IsEmpty => item == null;
        public List<ItemParameter> ItemState { get; set; }

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = item,
                quantity = newQuantity,
                ItemState = new List<ItemParameter>(ItemState)
            };
        }

        public static InventoryItem GetEmptyItem()
        {
            return new InventoryItem
            {
                item = null,
                quantity = 0,
                ItemState = new List<ItemParameter>()
            };
        }
    }
}