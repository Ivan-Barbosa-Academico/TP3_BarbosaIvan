using UnityEngine;
namespace TPShooter
{
    [CreateAssetMenu(menuName="TP Shooter/Weapon")]
    public sealed class WeaponData : ScriptableObject
    {
        [SerializeField] private string _displayName = "RIFLE";
        [SerializeField, Min(1)] private float _damage = 20;
        [SerializeField, Min(.1f)] private float _shotsPerSecond = 6;
        [SerializeField, Min(1)] private int _capacity = 30;
        [SerializeField, Min(.1f)] private float _reloadSeconds = 1.6f;
        [SerializeField, Min(1)] private float _range = 70;
        [SerializeField] private bool _automatic = true;
        [SerializeField, Min(1)] private float _projectileSpeed = 22;
        [SerializeField, Min(.1f)] private float _projectileLifetime = 5;
        [SerializeField] private AmmoData _ammo;
        public string DisplayName => _displayName;
        public float Damage => _damage;
        public float Interval => 1f / Mathf.Max(.1f,_shotsPerSecond);
        public int Capacity => _capacity;
        public float ReloadSeconds => _reloadSeconds;
        public float Range => _range;
        public bool Automatic => _automatic;
        public float ProjectileSpeed => _projectileSpeed;
        public float ProjectileLifetime => _projectileLifetime;
        public AmmoData Ammo => _ammo;
    }
}
