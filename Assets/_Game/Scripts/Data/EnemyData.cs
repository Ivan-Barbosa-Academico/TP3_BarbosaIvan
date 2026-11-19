using UnityEngine;
namespace TPShooter
{
    [CreateAssetMenu(menuName="TP Shooter/Enemy")]
    public sealed class EnemyData : ScriptableObject
    {
        [SerializeField,Min(1)] private float _health=60;
        [SerializeField,Min(.1f)] private float _patrolSpeed=2, _chaseSpeed=3.5f;
        [SerializeField,Min(1)] private float _detectionRange=12;
        [SerializeField,Range(0,360)] private float _viewAngle=100;
        [SerializeField,Min(.1f)] private float _attackRange=1.5f, _attackInterval=1;
        [SerializeField,Min(1)] private float _damage=10;
        [SerializeField,Min(.1f)] private float _memorySeconds=3;
        public float Health=>_health;
        public float PatrolSpeed=>_patrolSpeed;
        public float ChaseSpeed=>_chaseSpeed;
        public float DetectionRange=>_detectionRange;
        public float ViewAngle=>_viewAngle;
        public float AttackRange=>_attackRange;
        public float AttackInterval=>_attackInterval;
        public float Damage=>_damage;
        public float MemorySeconds=>_memorySeconds;
    }
}
