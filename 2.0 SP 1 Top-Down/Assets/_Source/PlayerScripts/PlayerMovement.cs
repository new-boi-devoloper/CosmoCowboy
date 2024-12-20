using UnityEngine;

namespace PlayerScripts
{
    public class PlayerMovement
    {
        public void Move(Rigidbody2D playerRb, Vector2 moveDirection, float playerSpeed)
        {
            playerRb.MovePosition(playerRb.position + moveDirection * (playerSpeed * Time.deltaTime));
        }
    }
}