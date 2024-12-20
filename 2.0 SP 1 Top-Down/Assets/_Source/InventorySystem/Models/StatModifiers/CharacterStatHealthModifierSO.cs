using EnemyScripts.EnemyManagement;
using UnityEngine;

namespace InventorySystem.Models.StatModifiers
{
    [CreateAssetMenu(fileName = "Stat EnemyHealth Modifier SO",
        menuName = "Modifiers SO/ Stat EnemyHealth Modifier SO")]
    public class CharacterStatHealthModifierSO : CharacterStatModifierSO
    {
        public override void AffectCharacter(GameObject character, float val)
        {
            var playerHealth = character.GetComponent<EnemyHealth>();
            if (playerHealth != null) playerHealth.ChangeHealth((int)val);
        }
    }
}