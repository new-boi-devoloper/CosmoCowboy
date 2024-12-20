using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Models
{
    public abstract class ItemSO : ScriptableObject
    {
        [field: SerializeField] public bool IsStackable { get; set; }
        [field: SerializeField] public int MaxStackSize { get; set; }
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public int Price { get; set; } // Добавляем свойство Price

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }

        [field: SerializeField] public Sprite ItemImage { get; set; }
        [field: SerializeField] public List<ItemParameter> DefaultParametersList { get; set; }
        public int Id => GetInstanceID();
    }

    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        public ItemParameterSO itemParameter;
        public float value;

        public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
    }
}