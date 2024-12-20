using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem.View
{
    public class UIInventoryItem :
        MonoBehaviour,
        IPointerClickHandler,
        IBeginDragHandler,
        IEndDragHandler,
        IDropHandler,
        IDragHandler
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text quantityTxt;
        [SerializeField] private Image borderImage;

        private bool _empty = true;

        private void Awake()
        {
            ResetData();
            Deselect();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_empty) return;
            OnItemBeginDrag?.Invoke(this);
        }

        //Unity obligates to have it in addition to OnEndDrag and OnBeginDrag
        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Clicked");
            var pointerData = eventData;

            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                Debug.Log("Right Clicked");
                OnRightMouseBtnClick?.Invoke(this);
            }

            if (pointerData.button == PointerEventData.InputButton.Left)
            {
                OnItemClicked?.Invoke(this);
                Debug.Log("Left Clicked");
            }
        }

        public event Action<UIInventoryItem>
            OnItemClicked,
            OnItemDroppedOn,
            OnItemBeginDrag,
            OnItemEndDrag,
            OnRightMouseBtnClick;

        public void ResetData()
        {
            itemImage.gameObject.SetActive(false);
            _empty = true;
        }

        public void Deselect()
        {
            borderImage.enabled = false;
        }

        public void Select()
        {
            borderImage.enabled = true;
        }

        public void SetData(Sprite sprite, int quantity)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            quantityTxt.text = quantity + "";
            _empty = false;
        }
    }
}