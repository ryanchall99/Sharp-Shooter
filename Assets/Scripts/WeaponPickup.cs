using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;

    const string PLAYER_STRING = "Player";

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>(); // Will always be a player due to IF check
            activeWeapon.SwitchWeapon(weaponSO);
            Destroy(this.gameObject);
        }
    }

    public void SwitchWeapon() 
    {
        Debug.Log(weaponSO.name);
    }
}
