using UnityEngine;
namespace TPShooter
{
    public sealed class ProjectileWeapon : WeaponController
    {
        [SerializeField] private Projectile _prefab;
        [SerializeField] private Transform _owner;
        private Collider[] _ownerColliders;
        protected override void Fire()
        {
            if(_ownerColliders==null)_ownerColliders=_owner.GetComponentsInChildren<Collider>();
            Vector3 target=AimPoint(),dir=(target-_muzzle.position).normalized;
            if(Physics.Raycast(_camera.position,(_muzzle.position-_camera.position).normalized,out var wall,Vector3.Distance(_camera.position,_muzzle.position),_hitMask,QueryTriggerInteraction.Ignore))
            {var h=wall.collider.GetComponentInParent<IDamageable>();bool damaged=h!=null&&h.IsAlive;if(damaged)h.TakeDamage(_data.Damage);ReportImpact(wall.point,damaged);return;}
            var projectile=Instantiate(_prefab,_muzzle.position,Quaternion.LookRotation(dir));
            projectile.Launch(dir*_data.ProjectileSpeed,_data.Damage,_data.ProjectileLifetime,_ownerColliders,ReportImpact);
        }
    }
}
