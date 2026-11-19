using UnityEngine;
namespace TPShooter
{
    [CreateAssetMenu(menuName = "TP Shooter/Player")]
    public sealed class PlayerStats : ScriptableObject
    {
        [SerializeField, Min(1)] private float _maxHealth = 100;
        [SerializeField, Min(.1f)] private float _speed = 5;
        [SerializeField, Min(.01f)] private float _sensitivity = .12f;
        [SerializeField, Range(30,89)] private float _pitchLimit = 85;
        [SerializeField] private float _gravity = -20;
        public float MaxHealth => _maxHealth;
        public float Speed => _speed;
        public float Sensitivity => _sensitivity;
        public float PitchLimit => _pitchLimit;
        public float Gravity => _gravity;
    }
}
