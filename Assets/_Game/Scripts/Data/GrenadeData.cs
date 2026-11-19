using UnityEngine;
namespace TPShooter
{
    [CreateAssetMenu(menuName="TP Shooter/Grenade")]
    public sealed class GrenadeData:ScriptableObject
    {
        [SerializeField,Min(0)] private int _count=3;
        [SerializeField,Min(.1f)] private float _fuse=2,_radius=5,_damage=80,_speed=12,_upward=3,_force=8;
        public int Count=>_count;
        public float Fuse=>_fuse;
        public float Radius=>_radius;
        public float Damage=>_damage;
        public float Speed=>_speed;
        public float Upward=>_upward;
        public float Force=>_force;
    }
}
