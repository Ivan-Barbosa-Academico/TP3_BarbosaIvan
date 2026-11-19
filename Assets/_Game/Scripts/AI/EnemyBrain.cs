using UnityEngine;
namespace TPShooter
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    public sealed class EnemyBrain : MonoBehaviour
    {
        public enum EnemyState { Patrol, Chase, Attack, Dead }
        [SerializeField] private EnemyData _data;
        [SerializeField] private EnemySensor _sensor;
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private Transform[] _points;
        private UnityEngine.AI.NavMeshAgent _agent;
        private int _point;
        private float _lastSeen=-100, _nextPath;
        private Vector3 _lastPosition;
        public EnemyState State { get; private set; }
        private void Awake(){_agent=GetComponent<UnityEngine.AI.NavMeshAgent>();}
        private void Start(){if(!_agent.isOnNavMesh)Debug.LogWarning("Enemigo fuera del NavMesh",this);}
        private void Update()
        {
            if(Time.timeScale==0||State==EnemyState.Dead||!_agent.isOnNavMesh)return;
            bool visible=_sensor.CanSee(State!=EnemyState.Patrol);
            if(visible){_lastSeen=Time.time;_lastPosition=_sensor.TargetPosition;}
            if(visible&&Vector3.Distance(transform.position,_sensor.TargetPosition)<=_data.AttackRange)
            {
                State=EnemyState.Attack;_agent.isStopped=true;
                Vector3 dir=_sensor.TargetPosition-transform.position;dir.y=0;if(dir.sqrMagnitude>.01f)transform.rotation=Quaternion.RotateTowards(transform.rotation,Quaternion.LookRotation(dir),360*Time.deltaTime);
                _attack.TryAttack();return;
            }
            _agent.isStopped=false;
            if(Time.time-_lastSeen<_data.MemorySeconds){State=EnemyState.Chase;_agent.speed=_data.ChaseSpeed;SetPath(_lastPosition);return;}
            State=EnemyState.Patrol;_agent.speed=_data.PatrolSpeed;
            if(_points==null||_points.Length==0){_agent.isStopped=true;return;}
            if(_points[_point]!=null)
            {
                Vector3 delta=_points[_point].position-transform.position;delta.y=0;
                if(!_agent.pathPending&&delta.sqrMagnitude<.36f)_point=(_point+1)%_points.Length;
            }
            if(_points[_point]!=null)SetPath(_points[_point].position);
        }
        private void SetPath(Vector3 point)
        {
            if(Time.time<_nextPath)return;_nextPath=Time.time+.3f;
            if(UnityEngine.AI.NavMesh.SamplePosition(point,out var hit,2,_agent.areaMask))_agent.SetDestination(hit.position);
        }
        public void Die(){State=EnemyState.Dead;if(_agent.isOnNavMesh){_agent.isStopped=true;_agent.ResetPath();}_agent.enabled=false;enabled=false;}
    }
}
