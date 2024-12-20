using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class PlayerUI : MonoBehaviour
    {
        [field: SerializeField] private Image playerHealthBar;
        [field: SerializeField] private Player player;
        

        private void OnEnable()
        {
            if (player != null)
            {
                player.OnHealthChanged += ChangeHealthView;
            }
            else
            {
                Debug.Log("player null");
            }
        }

        private void OnDisable()
        {
            if (player != null)
            {
                player.OnHealthChanged += ChangeHealthView;
            }
        }

        private void ChangeHealthView(int currentHealth, int maxHealth)
        {
            var healthPercentage = (float)currentHealth / maxHealth;

            playerHealthBar.fillAmount = healthPercentage;
        }
    }
}