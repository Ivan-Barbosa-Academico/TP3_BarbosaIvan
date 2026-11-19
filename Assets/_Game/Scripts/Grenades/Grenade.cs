using UnityEngine;
namespace TPShooter
{
    [RequireComponent(typeof(Rigidbody),typeof(Collider))]
    public sealed class Grenade:MonoBehaviour
    {
        [SerializeField] private GrenadeData _data;
        [SerializeField] private LayerMask _targets,_walls;
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private AudioClip _sound;
        private Rigidbody _body;
        private Collider _collider;
        private float _end;
        private bool _armed,_exploded;
        private void Awake(){_body=GetComponent<Rigidbody>();_collider=GetComponent<Collider>();}
        public void Launch(Vector3 velocity,Collider[] owner){foreach(var c in owner)Physics.IgnoreCollision(c,_collider);_body.linearVelocity=velocity;_body.angularVelocity=new Vector3(5,3,2);_end=Time.time+_data.Fuse;_armed=true;}
        private void Update(){if(_armed&&!_exploded&&Time.time>=_end)Explode();}
        private void Explode()
        {
            _exploded=true;ExplosionDamage.Apply(transform.position,_data,_targets,_walls);
            var effect=Instantiate(_effect,transform.position,Quaternion.identity);effect.Play();var audio=effect.gameObject.AddComponent<AudioSource>();audio.spatialBlend=.5f;audio.PlayOneShot(_sound,.7f);Destroy(effect.gameObject,2);Destroy(gameObject);
        }
    }
}
