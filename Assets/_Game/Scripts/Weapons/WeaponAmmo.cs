using System;
namespace TPShooter
{
    public sealed class WeaponAmmo
    {
        public int Loaded { get; private set; }
        public int Reserve { get; private set; }
        public int Capacity { get; }
        public int MaxReserve { get; }
        public bool CanReload => Loaded < Capacity && Reserve > 0;
        public WeaponAmmo(int capacity, int reserve, int maxReserve)
        {
            Capacity = Math.Max(1, capacity); MaxReserve = Math.Max(0,maxReserve);
            Loaded = Capacity; Reserve = Math.Max(0,Math.Min(reserve,MaxReserve));
        }
        public bool RefillToMaximum()
        {
            bool changed = Loaded < Capacity || Reserve < MaxReserve;
            Loaded = Capacity;
            Reserve = MaxReserve;
            return changed;
        }
        public bool Consume() { if (Loaded <= 0) return false; Loaded--; return true; }
        public void Reload() { int transfer = Math.Min(Capacity - Loaded, Reserve); Loaded += transfer; Reserve -= transfer; }
        public bool AddReserve(int amount) { int old=Reserve; Reserve=Math.Min(MaxReserve,Reserve+Math.Max(0,amount)); return old != Reserve; }
    }
}
