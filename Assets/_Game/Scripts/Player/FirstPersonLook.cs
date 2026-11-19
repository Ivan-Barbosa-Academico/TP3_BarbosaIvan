using UnityEngine;
namespace TPShooter
{
    public sealed class FirstPersonLook : MonoBehaviour
    {
        [SerializeField] private PlayerStats _stats;
        [SerializeField] private PlayerInputReader _input;
        [SerializeField] private Transform _camera;
        private float _pitch;
        private void LateUpdate()
        {
            Vector2 delta = _input.ConsumeLook();
            if (Time.timeScale == 0) return;
            transform.Rotate(0, delta.x * _stats.Sensitivity, 0);
            _pitch = Mathf.Clamp(_pitch - delta.y * _stats.Sensitivity, -_stats.PitchLimit, _stats.PitchLimit);
            _camera.localRotation = Quaternion.Euler(_pitch, 0, 0);
        }
    }
}
