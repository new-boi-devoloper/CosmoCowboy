﻿using System.Collections;
using InventorySystem.Models;
using UnityEngine;

namespace PickUpSystem
{
    public class Item : MonoBehaviour
    {
        [field: SerializeField] public ItemSO InventoryItem { get; internal set; }

        [field: SerializeField] public int Quantity { get; set; } = 1;

        [field: SerializeField] private AudioSource audioSource;

        [field: SerializeField] private float duration = 0.3f;

        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;
        }

        public void DestroyItem()
        {
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(AnimateItemPickup());
        }

        private IEnumerator AnimateItemPickup()
        {
            // audioSource.Play(); // место для звука подбора
            var startScale = transform.localScale;
            var endScale = Vector3.zero;
            float currentTime = 0;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                transform.localScale =
                    Vector3.Lerp(startScale, endScale, currentTime / duration);
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}