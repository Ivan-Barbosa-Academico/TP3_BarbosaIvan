using UnityEngine;
namespace TPShooter
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class FirstPersonMotor : MonoBehaviour
    {
        [SerializeField] private PlayerStats _stats;
        [SerializeField] private PlayerInputReader _input;
        private CharacterController _controller;
        private float _verticalSpeed;
        private void Awake() { _controller = GetComponent<CharacterController>(); }
        private void Update()
        {
            if (Time.timeScale == 0) return;
            if (_controller.isGrounded && _verticalSpeed < 0) _verticalSpeed = -2;
            _verticalSpeed += _stats.Gravity * Time.deltaTime;
            Vector2 move = _input.Move;
            Vector3 velocity = (transform.right * move.x + transform.forward * move.y) * _stats.Speed;
            velocity.y = _verticalSpeed;
            _controller.Move(velocity * Time.deltaTime);
        }
    }
}
