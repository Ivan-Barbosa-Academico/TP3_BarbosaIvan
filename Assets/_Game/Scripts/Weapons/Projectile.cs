using System;
using UnityEngine;
namespace TPShooter
{
    [RequireComponent(typeof(Rigidbody),typeof(Collider))]
    public sealed class Projectile : MonoBehaviour
    {
        private Rigidbody _body;
        private Collider _collider;
        private float _damage;
        private bool _spent;
        private Action<Vector3,bool> _impact;
        private void Awake(){_body=GetComponent<Rigidbody>();_collider=GetComponent<Collider>();}
        public void Launch(Vector3 velocity,float damage,float lifetime,Collider[] owner,Action<Vector3,bool> impact)
        {
            _damage=damage;_impact=impact;foreach(var c in owner)Physics.IgnoreCollision(_collider,c);
            _body.linearVelocity=velocity;Destroy(gameObject,lifetime);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if(_spent)return;_spent=true;
            var health=collision.collider.GetComponentInParent<IDamageable>();bool damaged=health!=null&&health.IsAlive;
            if(damaged)health.TakeDamage(_damage);
            _impact?.Invoke(collision.GetContact(0).point,damaged);Destroy(gameObject);
        }
    }
}
