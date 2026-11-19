using UnityEngine;
namespace TPShooter
{
    public sealed class HitscanWeapon : WeaponController
    {
        protected override void Fire()
        {
            Vector3 segment = _muzzle.position - _camera.position;
            if (Physics.Raycast(_camera.position,segment.normalized,out var wall,segment.magnitude,_hitMask,QueryTriggerInteraction.Ignore))
            {
                var receiver = wall.collider.GetComponentInParent<IDamageable>();
                bool damaged = receiver != null && receiver.IsAlive;
                if (damaged) receiver.TakeDamage(_data.Damage);
                ReportImpact(wall.point,damaged);
                return;
            }
            Vector3 target = AimPoint(); Vector3 direction = target - _muzzle.position;
            if (Physics.Raycast(_muzzle.position,direction.normalized,out var hit,direction.magnitude+.05f,_hitMask,QueryTriggerInteraction.Ignore))
            {
                var receiver=hit.collider.GetComponentInParent<IDamageable>();
                bool damaged=receiver!=null && receiver.IsAlive;
                if(damaged)receiver.TakeDamage(_data.Damage);
                ReportImpact(hit.point,damaged);
            }
        }
    }
}
