using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    public class InputListener : MonoBehaviour
    {
        private PlayerControls _playerControls;
        private PlayerInvoker _playerInvoker;

        public Vector2 MovementInput { get; private set; }

        private void Update()
        {
            SetMovement();
            _playerInvoker?.HandleInput(MovementInput);
        }

        private void OnDestroy()
        {
            _playerControls.Player.Fire.performed -= FireOnPerformed;
        }

        public static event Action OnFire;

        public void Initialize(PlayerControls playerControls, PlayerInvoker playerInvoker)
        {
            _playerControls = playerControls;
            _playerInvoker = playerInvoker;

            _playerControls.Player.Fire.performed += FireOnPerformed;
        }

        private static void FireOnPerformed(InputAction.CallbackContext obj)
        {
            OnFire?.Invoke();
        }

        private void SetMovement()
        {
            MovementInput = _playerControls.Player.Move.ReadValue<Vector2>();
        }
    }
}