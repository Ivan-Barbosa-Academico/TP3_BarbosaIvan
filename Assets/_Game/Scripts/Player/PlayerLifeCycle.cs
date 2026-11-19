using UnityEngine;
namespace TPShooter
{
    [RequireComponent(typeof(Health))]
    public sealed class PlayerLifeCycle : MonoBehaviour
    {
        [SerializeField] private PlayerStats _stats;
        private Health _health;
        private void Awake(){_health=GetComponent<Health>();}
        private void Start(){_health.Initialize(_stats.MaxHealth);}
    }
}
