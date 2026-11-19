using System;
using UnityEngine;
namespace TPShooter
{
    [RequireComponent(typeof(UnityEngine.InputSystem.PlayerInput))]
    public sealed class PlayerInputReader : MonoBehaviour
    {
        private UnityEngine.InputSystem.PlayerInput _input;
        private Vector2 _look;
        private bool _blocked;
        public Vector2 Move { get; private set; }
        public bool FireHeld { get; private set; }
        public event Action FirePressed, ReloadPressed, InteractPressed, GrenadePressed, PausePressed;
        public event Action<int> WeaponSelected;
        private void Awake() { _input = GetComponent<UnityEngine.InputSystem.PlayerInput>(); }
        private void OnEnable() { _input.onActionTriggered += OnAction; }
        private void OnDisable() { _input.onActionTriggered -= OnAction; Clear(); }
        public Vector2 ConsumeLook() { var value = _look; _look = Vector2.zero; return value; }
        public void SetBlocked(bool blocked) { _blocked = blocked; Clear(); }
        private void Clear() { Move = Vector2.zero; _look = Vector2.zero; FireHeld = false; }
        private void OnAction(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            string action = context.action.name;
            if (action == "Pause" && context.performed) { PausePressed?.Invoke(); return; }
            if (_blocked) return;
            if (action == "Move") Move = context.canceled ? Vector2.zero : Vector2.ClampMagnitude(context.ReadValue<Vector2>(), 1);
            if (action == "Look") _look = context.canceled ? Vector2.zero : context.ReadValue<Vector2>();
            if (action == "Attack") { FireHeld = !context.canceled; if (context.performed) FirePressed?.Invoke(); }
            if (!context.performed) return;
            switch (action)
            {
                case "Reload": ReloadPressed?.Invoke(); break;
                case "Interact": InteractPressed?.Invoke(); break;
                case "Grenade": GrenadePressed?.Invoke(); break;
                case "SelectWeapon1": WeaponSelected?.Invoke(0); break;
                case "SelectWeapon2": WeaponSelected?.Invoke(1); break;
            }
        }
    }
}
