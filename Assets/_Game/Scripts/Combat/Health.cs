using System;
using UnityEngine;
namespace TPShooter
{
    public sealed class Health : MonoBehaviour, IDamageable
    {
        [SerializeField, Min(1)] private float _maxHealth = 100;
        public float Current { get; private set; }
        public float Maximum => _maxHealth;
        public bool IsAlive => Current > 0;
        public event Action Changed, Died;
        public event Action<float> Damaged;
        private void Awake() { Current = _maxHealth; }
        public void Initialize(float maximum) { _maxHealth = Mathf.Max(1, maximum); Current = _maxHealth; Changed?.Invoke(); }
        public void TakeDamage(float amount)
        {
            if (!IsAlive || amount <= 0 || float.IsNaN(amount)) return;
            Current = Mathf.Max(0, Current - amount);
            Changed?.Invoke(); Damaged?.Invoke(amount);
            if (!IsAlive) Died?.Invoke();
        }
        public bool Heal(float amount)
        {
            if (!IsAlive || amount <= 0 || Current >= Maximum) return false;
            Current = Mathf.Min(Maximum, Current + amount); Changed?.Invoke(); return true;
        }
    }
}
