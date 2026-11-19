using UnityEngine;
namespace TPShooter
{
    public sealed class ExitZone : MonoBehaviour
    {
        [SerializeField] private GameSession _session;
        [SerializeField] private Health _player;
        [SerializeField] private bool _unlocked=true;
        public bool Unlocked=>_unlocked;
        public void Unlock(){_unlocked=true;}
        private void OnTriggerEnter(Collider other){TryExtract(other);}
        private void OnTriggerStay(Collider other){TryExtract(other);}
        private void TryExtract(Collider other){if(_unlocked&&other.GetComponentInParent<Health>()==_player)_session.Win();}
    }
}
