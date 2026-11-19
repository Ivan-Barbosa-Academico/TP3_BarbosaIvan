using UnityEngine;
namespace TPShooter
{
    public sealed class WeaponFeedback : MonoBehaviour
    {
        [SerializeField] private WeaponController _weapon;
        [SerializeField] private AudioSource _audio;
        [SerializeField] private AudioClip _shot, _reload, _impactSound;
        [SerializeField] private ParticleSystem _flash, _impactPrefab;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private LineRenderer _tracer;
        [SerializeField] private UnityEngine.UI.Image _hitMarker;
        [SerializeField,Min(.01f)] private float _recoil=.06f;
        private Vector3 _rest;
        private float _traceEnd, _markerEnd;
        private bool _wasReloading;
        private void Awake(){_rest=transform.localPosition;}
        private void OnEnable(){_weapon.Shot+=Shot;_weapon.Changed+=Changed;_weapon.Impact+=Impact;}
        private void OnDisable(){_weapon.Shot-=Shot;_weapon.Changed-=Changed;_weapon.Impact-=Impact;_tracer.enabled=false;transform.localPosition=_rest;}
        private void Shot(){_audio.PlayOneShot(_shot,.5f);_flash.Play();transform.localPosition=_rest+Vector3.back*_recoil;}
        private void Changed(){if(_weapon.Reloading&&!_wasReloading)_audio.PlayOneShot(_reload,.4f);_wasReloading=_weapon.Reloading;}
        private void Impact(Vector3 position,bool damaged)
        {
            if(this==null)return;
            var effect=Instantiate(_impactPrefab,position,Quaternion.identity);effect.Play();Destroy(effect.gameObject,1);
            _audio.PlayOneShot(_impactSound,.25f);
            if(_weapon is HitscanWeapon){_tracer.SetPosition(0,_muzzle.position);_tracer.SetPosition(1,position);_traceEnd=Time.time+.05f;_tracer.enabled=true;}
            if(damaged){_markerEnd=Time.unscaledTime+.15f;_hitMarker.enabled=true;}
        }
        private void Update(){transform.localPosition=Vector3.Lerp(transform.localPosition,_rest,Time.deltaTime*18);if(Time.time>_traceEnd)_tracer.enabled=false;if(Time.unscaledTime>_markerEnd)_hitMarker.enabled=false;}
    }
}
