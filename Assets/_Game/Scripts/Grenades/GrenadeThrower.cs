using UnityEngine;
namespace TPShooter
{
    public sealed class GrenadeThrower:MonoBehaviour
    {
        [SerializeField] private PlayerInputReader _input;
        [SerializeField] private GrenadeData _data;
        [SerializeField] private Grenade _prefab;
        [SerializeField] private Transform _camera;
        [SerializeField] private TMPro.TMP_Text _text;
        [SerializeField] private LayerMask _walls;
        private Collider[] _owner;
        public int Remaining {get;private set;}
        private void Awake(){_owner=GetComponentsInChildren<Collider>();Remaining=_data.Count;}
        private void OnEnable(){_input.GrenadePressed+=Throw;}
        private void Start(){Refresh();}
        private void OnDisable(){_input.GrenadePressed-=Throw;}
        private void Refresh(){_text.SetText("G: GRANADAS  {0}",Remaining);}
        public void Throw()
        {
            if(Remaining<=0||Time.timeScale==0)return;
            Vector3 start=_camera.position+_camera.forward*.7f;
            if(Physics.SphereCast(_camera.position,.15f,_camera.forward,out var hit,.7f,_walls,QueryTriggerInteraction.Ignore))start=hit.point-_camera.forward*.2f;
            var grenade=Instantiate(_prefab,start,Quaternion.identity);grenade.Launch(_camera.forward*_data.Speed+Vector3.up*_data.Upward,_owner);Remaining--;Refresh();
        }
    }
}
