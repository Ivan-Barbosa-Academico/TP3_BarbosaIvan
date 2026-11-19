using UnityEngine;
namespace TPShooter
{
    public sealed class EnemySensor : MonoBehaviour
    {
        [SerializeField] private EnemyData _data;
        [SerializeField] private Health _target;
        [SerializeField] private Transform _eyes;
        [SerializeField] private LayerMask _visionMask;
        public Health Target=>_target;
        public Vector3 TargetPosition=>_target.transform.position;
        public bool CanSee(bool alerted)
        {
            if(_target==null||!_target.IsAlive)return false;
            Vector3 delta=_target.transform.position+Vector3.up - _eyes.position;
            if(delta.magnitude>_data.DetectionRange)return false;
            if(!alerted && Vector3.Angle(transform.forward,delta)>_data.ViewAngle*.5f)return false;
            return HasLineOfSight();
        }
        public bool HasLineOfSight()
        {
            if(_target==null||!_target.IsAlive)return false;
            Vector3 delta=_target.transform.position+Vector3.up-_eyes.position;
            if(Physics.Raycast(_eyes.position,delta.normalized,out var hit,delta.magnitude+.1f,_visionMask,QueryTriggerInteraction.Ignore))return hit.collider.GetComponentInParent<Health>()==_target;
            return false;
        }
    }
}
