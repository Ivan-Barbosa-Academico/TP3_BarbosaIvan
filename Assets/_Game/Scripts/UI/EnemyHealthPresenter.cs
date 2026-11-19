using UnityEngine;
namespace TPShooter
{
    public sealed class EnemyHealthPresenter : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private Transform _camera;
        [SerializeField] private UnityEngine.UI.Slider _bar;
        [SerializeField] private TMPro.TMP_Text _text;
        [SerializeField] private GameObject _visual;
        private void OnEnable(){_health.Changed+=Refresh;}
        private void Start(){Refresh();}
        private void OnDisable(){_health.Changed-=Refresh;}
        private void Refresh()
        {
            _bar.normalizedValue=Mathf.Clamp01(_health.Current/_health.Maximum);
            _text.SetText("{0} / {1}",Mathf.Ceil(_health.Current),Mathf.Ceil(_health.Maximum));
            _visual.SetActive(_health.IsAlive);
        }
        private void LateUpdate()
        {
            if(_camera!=null)_visual.transform.rotation=_camera.rotation;
        }
    }
}
