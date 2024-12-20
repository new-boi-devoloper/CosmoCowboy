using UnityEngine;

namespace InventorySystem.Models
{
    [CreateAssetMenu]
    public class ItemParameterSO : ScriptableObject
    {
        [field: SerializeField] public string ParameterName { get; private set; }
    }
}