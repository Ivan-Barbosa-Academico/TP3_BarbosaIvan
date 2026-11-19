using UnityEngine;
namespace TPShooter
{
    [CreateAssetMenu(menuName="TP Shooter/Ammo")]
    public sealed class AmmoData : ScriptableObject
    {
        [SerializeField, Min(0)] private int _initialReserve = 90;
        [SerializeField, Min(1)] private int _maxReserve = 180;
        [SerializeField, Min(1)] private int _pickupAmount = 30;
        public int InitialReserve => Mathf.Min(_initialReserve, _maxReserve);
        public int MaxReserve => _maxReserve;
        public int PickupAmount => _pickupAmount;
    }
}
