using System.Collections.Generic;
using System.Text;
using InventorySystem.Models;
using InventorySystem.View;
using UnityEngine;

namespace InventorySystem.Controller
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private UIInventoryPage inventoryUI;

        [SerializeField] private InventorySO inventoryData;
        public List<InventoryItem> initialItems = new();

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventoryUI.isActiveAndEnabled == false)
                {
                    Debug.Log("opened Inventory");
                    inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(
                            item.Key,
                            item.Value.item.ItemImage,
                            item.Value.quantity);
                        Debug.Log($"Showed inventory slot{item}");
                    }

                    Debug.Log("finished loading inventory");
                }
                else
                {
                    inventoryUI.Hide();
                }
            }
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (var item in initialItems)
            {
                if (item.IsEmpty) continue;

                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
                inventoryUI.UpdateData(
                    item.Key,
                    item.Value.item.ItemImage,
                    item.Value.quantity);
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleOnSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            var inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty) return;

            var itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                inventoryUI.ShowItemAction(itemIndex);
                Debug.Log("1");
                inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
                Debug.Log("1.1");

                // itemAction.PerformAction(gameObject, inventoryItem.ItemState);
            }

            var destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
                inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
        }

        private void DropItem(int itemIndex, int inventoryItemQuantity)
        {
            inventoryData.RemoveItem(itemIndex, inventoryItemQuantity);
            inventoryUI.ResetSelection();
            Debug.Log("dropped item");
        }

        public void PerformAction(int itemIndex)
        {
            var inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            var destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null) inventoryData.RemoveItem(itemIndex, 1);

            var itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject, inventoryItem.ItemState);
                Debug.Log("Performed action");
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                {
                    Debug.Log("2");

                    inventoryUI.ResetSelection();
                }
            }
        }

        private void HandleDragging(int itemIndex)
        {
            var inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty) return;

            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleOnSwapItems(int itemIndex1, int itemIndex2)
        {
            inventoryData.SwapItems(itemIndex1, itemIndex2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            var inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }

            var item = inventoryItem.item;
            var description = PrepareDescription(inventoryItem);
            inventoryUI.UpdateDescription(
                itemIndex,
                item.ItemImage,
                item.Name,
                description);
        }

        private string PrepareDescription(InventoryItem inventoryItem)
        {
            var sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();
            for (var i = 0; i < inventoryItem.ItemState.Count; i++)
            {
                sb.Append(
                    $"{inventoryItem.ItemState[i].itemParameter.ParameterName} " +
                    $": {inventoryItem.ItemState[i].value} / " +
                    $"{inventoryItem.item.DefaultParametersList[i].value}");
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}