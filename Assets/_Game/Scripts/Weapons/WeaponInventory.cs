using System;
using UnityEngine;
namespace TPShooter
{
    public sealed class WeaponInventory : MonoBehaviour
    {
        [SerializeField] private PlayerInputReader _input;
        [SerializeField] private WeaponController[] _weapons;
        public int Selected { get; private set; }
        public WeaponController Current => _weapons[Selected];
        public event Action Changed;
        private void Awake() { for(int i=0;i<_weapons.Length;i++) { _weapons[i].Initialize(); _weapons[i].gameObject.SetActive(i==Selected); } }
        private void OnEnable(){ _input.FirePressed+=Fire; _input.ReloadPressed+=Reload; _input.WeaponSelected+=Select; foreach(var w in _weapons)w.Changed+=Notify; }
        private void OnDisable() { _input.FirePressed-=Fire; _input.ReloadPressed-=Reload; _input.WeaponSelected-=Select; foreach(var w in _weapons)w.Changed-=Notify; }
        private void Update() { if (_input.FireHeld && Current.Data.Automatic) Current.TryFire(); }
        private void Fire() { Current.TryFire(); }
        private void Reload() { Current.BeginReload(); }
        private void Notify() { Changed?.Invoke(); }
        public void Select(int index)
        {
            if (Time.timeScale==0 || index<0 || index>=_weapons.Length || index==Selected) return;
            Current.CancelReload(); Current.gameObject.SetActive(false); Selected=index; Current.gameObject.SetActive(true); Changed?.Invoke();
        }
        public bool RefillAllWeapons()
        {
            bool changed = false;
            foreach (var weapon in _weapons)
            {
                if (weapon.RefillToMaximum()) changed = true;
            }
            return changed;
        }
        public bool AddAmmo(int index) { if(index<0||index>=_weapons.Length)return false; return _weapons[index].AddAmmo(_weapons[index].Data.Ammo.PickupAmount); }
    }
}
