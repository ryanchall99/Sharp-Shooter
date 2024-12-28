using StarterAssets;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    // --- WEAPON STATS ---
    [Header("Weapon Stats")]
    [SerializeField] WeaponSO weaponSO;

    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    Weapon currentWeapon;

    const string SHOOT_STRING = "Shoot";

    float timeSinceLastShot = 0f;

    void Awake() 
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    void Start() 
    {
        currentWeapon = GetComponentInChildren<Weapon>();  
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime; // Fire Rate Logic
        HandleShoot();
    }

    public void SwitchWeapon(WeaponSO weaponSO) 
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }

        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
        // Update Weapon & Weapon Stats (WeaponSO)
        currentWeapon = newWeapon;
        this.weaponSO = weaponSO;
    }

    void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;

        if (timeSinceLastShot >= weaponSO.FireRate)
        {
            currentWeapon.Shoot(weaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            timeSinceLastShot = 0f; // Reset Timer
        }

        if (!weaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }
    }
}
