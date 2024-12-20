using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.View
{
    public class ItemActionPanel : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab;

        public void AddButon(string name, Action onClickAction)
        {
            var button = Instantiate(buttonPrefab, transform);
            button.GetComponent<Button>().onClick.AddListener(() => onClickAction());
            button.GetComponentInChildren<TMP_Text>().text = name;
        }

        public void Toggle(bool val)
        {
            if (val)
                RemoveOldButtons();
            Debug.Log($"maded item action object{val}");
            gameObject.SetActive(val);
        }

        public void RemoveOldButtons()
        {
            foreach (Transform transformChildObjects in transform) Destroy(transformChildObjects.gameObject);
        }
    }
}