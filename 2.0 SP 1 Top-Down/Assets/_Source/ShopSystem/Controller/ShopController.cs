using InventorySystem.Models;
using PlayerScripts;
using ShopSystem.View;
using UnityEngine;

namespace ShopSystem.Controller
{
    public class ShopController : MonoBehaviour
    {
        [field: SerializeField] private ShopView shopView;
        [field: SerializeField] private InventorySO playerInventory;
        [field: SerializeField] private Player player;

        private InventorySO _sellerInventory;

        private void Start()
        {
            if (shopView == null) Debug.LogError("ShopView is not assigned in the inspector.");
            if (playerInventory == null) Debug.LogError("PlayerInventory is not assigned in the inspector.");
            if (player == null) Debug.LogError("Player is not assigned in the inspector.");

            shopView.Initialize(this);
        }

        public void OpenShop(InventorySO sellerInventory)
        {
            _sellerInventory = sellerInventory;
            DisplayShop();
        }

        private void DisplayShop()
        {
            shopView.Show();
            ShowInventoryInfo();
        }

        private void ShowInventoryInfo()
        {
            shopView.UpdateInventory(_sellerInventory, playerInventory);
        }

        public void BuyItem(int itemIndex)
        {
            var itemToBuy = _sellerInventory.GetItemAt(itemIndex);
            if (itemToBuy.IsEmpty) return;

            if (player.Money >= itemToBuy.item.Price)
            {
                player.TryChangeCoins(-itemToBuy.item.Price);
                playerInventory.AddItem(itemToBuy);
                _sellerInventory.RemoveItem(itemIndex, 1);
                ShowInventoryInfo();
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }

        public void SellItem(int itemIndex)
        {
            var itemToSell = playerInventory.GetItemAt(itemIndex);
            if (itemToSell.IsEmpty) return;

            player.TryChangeCoins(itemToSell.item.Price);

            _sellerInventory.AddItem(itemToSell);
            playerInventory.RemoveItem(itemIndex, 1);
            ShowInventoryInfo();
        }

        public void SelectItem(int itemIndex, bool isSellerItem)
        {
            var item = isSellerItem ? _sellerInventory.GetItemAt(itemIndex) : playerInventory.GetItemAt(itemIndex);
            if (item.IsEmpty) return;

            Debug.Log($"Selected item: {item.item.Name}");
        }
    }
}