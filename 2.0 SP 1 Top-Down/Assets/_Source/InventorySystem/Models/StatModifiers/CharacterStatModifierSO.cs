using UnityEngine;

namespace InventorySystem.Models.StatModifiers
{
    public abstract class CharacterStatModifierSO : ScriptableObject
    {
        public abstract void AffectCharacter(GameObject character, float val);
    }
}