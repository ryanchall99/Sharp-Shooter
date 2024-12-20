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
        HandleShoot();
    }

    void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;

        currentWeapon.Shoot(weaponSO);
        animator.Play(SHOOT_STRING, 0, 0f);
        starterAssetsInputs.ShootInput(false);
    }
}
