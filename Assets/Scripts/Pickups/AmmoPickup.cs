using UnityEngine;

public class AmmoPickup : BasePickup
{
    [SerializeField] int ammoAmount = 100; // Always fills up weapons to max

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.AdjustAmmo(ammoAmount);
    }
}
