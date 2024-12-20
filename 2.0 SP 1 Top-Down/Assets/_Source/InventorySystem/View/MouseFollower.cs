using UnityEngine;

namespace InventorySystem.View
{
    public class MouseFollower : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Camera mainCamera;

        [SerializeField] private UIInventoryItem item;

        private void Awake()
        {
            canvas = transform.root.GetComponent<Canvas>();
            mainCamera = Camera.main;
            item = GetComponentInChildren<UIInventoryItem>();
        }

        private void Update()
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                Input.mousePosition,
                canvas.worldCamera,
                out position);
            transform.position = canvas.transform.TransformPoint(position);
        }

        public void SetData(Sprite sprite, int quantity)
        {
            item.SetData(sprite, quantity);
        }

        public void Toggle(bool val)
        {
            gameObject.SetActive(val);
        }
    }
}