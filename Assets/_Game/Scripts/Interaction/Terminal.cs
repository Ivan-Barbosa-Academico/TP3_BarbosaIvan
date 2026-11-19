using UnityEngine;
namespace TPShooter
{
    public sealed class Terminal : MonoBehaviour,IInteractable
    {
        [SerializeField] private ObjectiveController _objective;
        [SerializeField] private Renderer _screen;
        [SerializeField] private Material _activeMaterial;
        [SerializeField] private AudioSource _audio;
        public string Prompt=>"Activar terminal";
        public bool Available=>!_objective.Completed;
        public void Interact(){if(!Available||Time.timeScale==0)return;_objective.Activate();_screen.sharedMaterial=_activeMaterial;if(_audio!=null)_audio.Play();}
    }
}
