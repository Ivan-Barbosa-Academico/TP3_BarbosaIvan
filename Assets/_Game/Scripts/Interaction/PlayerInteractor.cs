using UnityEngine;
namespace TPShooter
{
    public sealed class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private PlayerInputReader _input;
        [SerializeField] private Transform _camera;
        [SerializeField] private TMPro.TMP_Text _prompt;
        [SerializeField] private LayerMask _mask;
        [SerializeField,Min(.1f)] private float _range=3;
        private IInteractable _target;
        private void OnEnable(){_input.InteractPressed+=Interact;}
        private void OnDisable(){_input.InteractPressed-=Interact;}
        private void Update(){Scan();}
        private void Scan()
        {
            _target=null;
            if(Time.timeScale>0&&Physics.Raycast(_camera.position,_camera.forward,out var hit,_range,_mask,QueryTriggerInteraction.Ignore))_target=hit.collider.GetComponentInParent<IInteractable>();
            if(_target!=null&&!_target.Available)_target=null;
            _prompt.text=_target==null?"":"[E] "+_target.Prompt;
        }
        private void Interact(){Scan();_target?.Interact();Scan();}
    }
}
