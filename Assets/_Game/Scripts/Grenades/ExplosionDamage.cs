using System.Collections.Generic;
using UnityEngine;
namespace TPShooter
{
    public static class ExplosionDamage
    {
        public static void Apply(Vector3 origin,GrenadeData data,LayerMask targets,LayerMask walls)
        {
            var seen=new HashSet<Health>();var bodies=new HashSet<Rigidbody>();
            foreach(var collider in Physics.OverlapSphere(origin,data.Radius,targets,QueryTriggerInteraction.Ignore))
            {
                var health=collider.GetComponentInParent<Health>();
                if(health!=null&&seen.Add(health)&&health.IsAlive)
                {
                    Vector3 center=health.transform.position+Vector3.up*.5f;
                    Vector3 delta=center-origin;float distance=delta.magnitude;
                    if(distance<data.Radius&&!Physics.Raycast(origin,delta.normalized,distance,walls,QueryTriggerInteraction.Ignore))
                    {
                        bool enemy=health.GetComponent<EnemyBrain>()!=null;
                        health.TakeDamage(enemy?health.Current:data.Damage*(1-distance/data.Radius));
                    }
                }
                var rb=collider.attachedRigidbody;
                if(rb!=null&&!rb.isKinematic&&bodies.Add(rb))rb.AddExplosionForce(data.Force,origin,data.Radius,1,ForceMode.Impulse);
            }
        }
    }
}
