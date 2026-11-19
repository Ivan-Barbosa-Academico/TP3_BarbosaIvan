using UnityEngine;
namespace TPShooter
{
    public sealed class DamageFeedback : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private AudioSource _audio;
        [SerializeField] private AudioClip _clip;
        [SerializeField] private UnityEngine.UI.Image[] _edges;
        private float _alpha;
        private void OnEnable(){_health.Damaged+=Damaged;}
        private void OnDisable(){_health.Damaged-=Damaged;}
        private void Damaged(float amount){_alpha=.45f;_audio.PlayOneShot(_clip,.6f);}
        private void Update(){_alpha=Mathf.MoveTowards(_alpha,0,Time.unscaledDeltaTime*1.5f);foreach(var edge in _edges)edge.color=new Color(1,.05f,.02f,_alpha);}
    }
}
