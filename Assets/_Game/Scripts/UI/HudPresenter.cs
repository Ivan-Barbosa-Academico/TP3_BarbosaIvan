using UnityEngine;
namespace TPShooter
{
    public sealed class HudPresenter : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private WeaponInventory _inventory;
        [SerializeField] private TMPro.TMP_Text _healthText, _ammoText;
        [SerializeField] private UnityEngine.UI.Slider _healthBar;
        private void OnEnable(){_health.Changed+=Refresh;_inventory.Changed+=Refresh;}
        private void Start(){Refresh();}
        private void OnDisable(){_health.Changed-=Refresh;_inventory.Changed-=Refresh;}
        private void Refresh()
        {
            _healthText.SetText("VIDA  {0}",Mathf.Ceil(_health.Current));_healthBar.value=_health.Current/_health.Maximum;
            var w=_inventory.Current;if(w.Ammo==null)return;
            _ammoText.text=$"{w.Data.DisplayName}    {w.Ammo.Loaded} / {w.Ammo.Reserve}"+(w.Reloading?"\nRECARGANDO...":"");
        }
    }
}
