using UnityEngine;
namespace TPShooter
{
    [RequireComponent(typeof(Health))]
    public sealed class EnemyLifeCycle : MonoBehaviour
    {
        [SerializeField] private EnemyData _data;
        [SerializeField] private EnemyBrain _brain;
        [SerializeField] private Renderer[] _renderers;
        [SerializeField] private Material _deadMaterial;
        private Health _health;
        private Collider[] _colliders;
        private void Awake(){_health=GetComponent<Health>();_colliders=GetComponentsInChildren<Collider>();}
        private void OnEnable(){_health.Died+=Die;}
        private void Start(){_health.Initialize(_data.Health);}
        private void OnDisable(){_health.Died-=Die;}
        private void Die()
        {
            _brain.Die();foreach(var c in _colliders)c.enabled=false;
            foreach(var r in _renderers)r.sharedMaterial=_deadMaterial;
            transform.localScale*=.65f;Destroy(gameObject,1.5f);
        }
    }
}
