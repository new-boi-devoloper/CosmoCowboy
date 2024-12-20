using InventorySystem.Models;
using InventorySystem.View;
using ShopSystem.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem.View
{
    public class ShopView : MonoBehaviour
    {
        [field: SerializeField] private GameObject shopUI;
        [field: SerializeField] private GameObject sellerContent;
        [field: SerializeField] private GameObject playerContent;
        [field: SerializeField] private GameObject itemPrefab;

        private ShopController _shopController;

        public void Initialize(ShopController shopController)
        {
            _shopController = shopController;
        }

        public void Show()
        {
            shopUI.SetActive(true);
        }

        public void Hide()
        {
            shopUI.SetActive(false);
        }

        public void UpdateInventory(InventorySO sellerInventorySo, InventorySO playerInventorySo)
        {
            if (sellerContent == null || playerContent == null)
            {
                Debug.LogError("sellerContent or playerContent is not assigned in the inspector.");
                return;
            }

            ClearContent(sellerContent);
            ClearContent(playerContent);

            foreach (var inventoryItem in sellerInventorySo.GetCurrentInventoryState())
                AddItemToContent(sellerContent, inventoryItem.Value, true);

            foreach (var inventoryItem in playerInventorySo.GetCurrentInventoryState())
                AddItemToContent(playerContent, inventoryItem.Value, false);
        }

        private void ClearContent(GameObject content)
        {
            foreach (Transform child in content.transform) Destroy(child.gameObject);
        }

        private void AddItemToContent(GameObject content, InventoryItem item, bool isSellerItem)
        {
            if (itemPrefab == null)
            {
                Debug.LogError("itemPrefab is not assigned in the inspector.");
                return;
            }

            var itemUI = Instantiate(itemPrefab, content.transform);
            itemUI.GetComponent<Image>().sprite = item.item.ItemImage;
            itemUI.GetComponentInChildren<TMP_Text>().text = $"{item.item.Name} x{item.quantity}";

            var uiInventoryItem = itemUI.GetComponent<UIInventoryItem>();
            uiInventoryItem.SetData(item.item.ItemImage, item.quantity);

            uiInventoryItem.OnItemClicked += clickedItem =>
            {
                var index = content.transform.GetSiblingIndex();
                _shopController.SelectItem(index, isSellerItem);
            };

            uiInventoryItem.OnRightMouseBtnClick += clickedItem =>
            {
                var index = content.transform.GetSiblingIndex();
                if (isSellerItem)
                    _shopController.BuyItem(index);
                else
                    _shopController.SellItem(index);
            };
        }
    }
}