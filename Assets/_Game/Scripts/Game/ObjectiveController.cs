using UnityEngine;
namespace TPShooter
{
    public sealed class ObjectiveController : MonoBehaviour
    {
        [SerializeField] private DoorController _door;
        [SerializeField] private ExitZone _exit;
        [SerializeField] private TMPro.TMP_Text _text;
        public bool Completed { get; private set; }
        private void Start(){_text.text="OBJETIVO: activar terminal del sector A\nE interactuar - 1/2 armas - G granada";}
        public void Activate()
        {
            if(Completed)return;Completed=true;_door.Open();_exit.Unlock();
            _text.text="OBJETIVO: llegar a EXTRACCION\nPuerta abierta. Avanza al sector B.";
        }
    }
}
