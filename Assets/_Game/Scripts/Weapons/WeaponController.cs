using System;
using UnityEngine;
namespace TPShooter
{
    public abstract class WeaponController : MonoBehaviour
    {
        [SerializeField] protected WeaponData _data;
        [SerializeField] protected Transform _camera;
        [SerializeField] protected Transform _muzzle;
        [SerializeField] protected LayerMask _hitMask;
        private float _nextShot, _reloadEnd;
        public WeaponData Data => _data;
        public WeaponAmmo Ammo { get; private set; }
        public bool Reloading { get; private set; }
        public event Action Changed, Shot;
        public event Action<Vector3,bool> Impact;
        private void Awake() { Initialize(); }
        public void Initialize() { if (Ammo == null) Ammo = new WeaponAmmo(_data.Capacity,_data.Ammo.InitialReserve,_data.Ammo.MaxReserve); }
        protected virtual void Update()
        {
            if (Time.timeScale == 0) return;
            if (Reloading && Time.time >= _reloadEnd) { Reloading=false; Ammo.Reload(); Changed?.Invoke(); }
        }
        protected virtual void OnDisable() { CancelReload(); }
        public bool TryFire()
        {
            if (Time.timeScale == 0 || Reloading || Time.time < _nextShot || !Ammo.Consume()) return false;
            _nextShot = Time.time + _data.Interval;
            Fire(); Shot?.Invoke(); Changed?.Invoke(); return true;
        }
        public bool BeginReload()
        {
            if (Time.timeScale == 0 || Reloading || !Ammo.CanReload) return false;
            Reloading=true; _reloadEnd=Time.time + _data.ReloadSeconds; Changed?.Invoke(); return true;
        }
        public void CancelReload() { if (!Reloading) return; Reloading=false; Changed?.Invoke(); }
        public bool RefillToMaximum()
        {
            Initialize();
            bool changed = Ammo.RefillToMaximum();
            if (!changed) return false;
            Reloading = false;
            Changed?.Invoke();
            return true;
        }
        public bool AddAmmo(int amount) { bool added=Ammo.AddReserve(amount); if(added)Changed?.Invoke(); return added; }
        protected abstract void Fire();
        protected void ReportImpact(Vector3 point, bool damaged) { Impact?.Invoke(point,damaged); }
        protected Vector3 AimPoint()
        {
            if (Physics.Raycast(_camera.position,_camera.forward,out var hit,_data.Range,_hitMask,QueryTriggerInteraction.Ignore)) return hit.point;
            return _camera.position + _camera.forward * _data.Range;
        }
    }
}
