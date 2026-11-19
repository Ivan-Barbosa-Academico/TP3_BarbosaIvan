using UnityEngine;
namespace TPShooter
{
    public sealed class Pickup : MonoBehaviour,IInteractable
    {
        [SerializeField] private Health _player;
        [SerializeField] private WeaponInventory _inventory;
        [SerializeField,Min(1)] private float _healAmount=50;
        private bool _taken;
        public string Prompt=>"Recoger 50 de vida y municion completa";
        public bool Available=>!_taken;
        public void Interact()
        {
            if(_taken||Time.timeScale==0)return;
            bool healed=_player.Heal(_healAmount);
            bool refilled=_inventory.RefillAllWeapons();
            if(healed||refilled){_taken=true;Destroy(gameObject);}
        }
    }
}
