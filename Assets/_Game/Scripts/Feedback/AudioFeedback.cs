using UnityEngine;
namespace TPShooter
{
    public sealed class AudioFeedback : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip _clip;
        public void Play(){if(_source!=null&&_clip!=null)_source.PlayOneShot(_clip,.4f);}
    }
}
