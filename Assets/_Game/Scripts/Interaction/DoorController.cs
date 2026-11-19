using UnityEngine;
namespace TPShooter
{
    public sealed class DoorController : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private UnityEngine.AI.NavMeshObstacle _obstacle;
        [SerializeField] private Transform _visual;
        [SerializeField,Min(.1f)] private float _openSpeed=4;
        public bool IsOpen { get; private set; }
        private Vector3 _closed;
        private void Awake(){_closed=_visual.localPosition;}
        public void Open(){if(IsOpen)return;IsOpen=true;_collider.enabled=false;_obstacle.enabled=false;}
        private void Update(){if(IsOpen)_visual.localPosition=Vector3.MoveTowards(_visual.localPosition,_closed+Vector3.up*5,_openSpeed*Time.deltaTime);}
    }
}
