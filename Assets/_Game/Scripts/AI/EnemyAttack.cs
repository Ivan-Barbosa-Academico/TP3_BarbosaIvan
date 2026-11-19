using UnityEngine;
namespace TPShooter
{
    public sealed class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyData _data;
        [SerializeField] private EnemySensor _sensor;
        private float _nextAttack;
        public void TryAttack()
        {
            if(Time.timeScale==0||Time.time<_nextAttack||!_sensor.HasLineOfSight())return;
            if(Vector3.Distance(transform.position,_sensor.TargetPosition)>_data.AttackRange)return;
            _nextAttack=Time.time+_data.AttackInterval;_sensor.Target.TakeDamage(_data.Damage);
        }
    }
}
