using Cinemachine;
using StarterAssets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    // --- WEAPON STATS ---
    [Header("Weapon Stats")]
    [SerializeField] WeaponSO startingWeapon;

    [Header("Weapon Zoom")]
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] Camera weaponCamera;
    [SerializeField] GameObject zoomVignette;

    [Header("Weapon UI")]
    [SerializeField] TMP_Text ammoText;

    /* --- PLAYER --- */
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    FirstPersonController firstPersonController;
    
    /* --- WEAPON --- */
    Weapon currentWeapon;
    WeaponSO currentWeaponSO;

    const string SHOOT_STRING = "Shoot";

    float timeSinceLastShot = 0f;
    float defaultFOV;
    float defaultRotationSpeed;
    int currentAmmo;

    void Awake() 
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();

        // Default Values
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }

    void Start() 
    {
        SwitchWeapon(startingWeapon);
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }

    void Update()
    {
        HandleShoot();
        HandleZoom();
    }

    /* --- PUBLIC METHODS ---*/
    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount; // Adjust Ammo By Amount Passed In

        // If Current Ammo is larger than magazine size
        if (currentAmmo > currentWeaponSO.MagazineSize)
        {
            currentAmmo = currentWeaponSO.MagazineSize; // Set ammo to max it can have
        }

        ammoText.text = currentAmmo.ToString("D2"); // Update UI Text NOTE: D2 = Formats To Always 2 Digits
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
        this.currentWeaponSO = weaponSO;

        AdjustAmmo(currentWeaponSO.MagazineSize);
    }

    /* --- PRIVATE METHODS --- */
    void HandleShoot()
    {
        timeSinceLastShot += Time.deltaTime; // Fire Rate Logic

        if (!starterAssetsInputs.shoot) return;

        if (timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
            AdjustAmmo(-1);
            currentWeapon.Shoot(currentWeaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            timeSinceLastShot = 0f; // Reset Timer
        }

        // If Weapon Is Not Automatic
        if (!currentWeaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }
    }

    void HandleZoom()
    {
        if (!currentWeaponSO.CanZoom) return; // Cannot Zoom In

        if (starterAssetsInputs.zoom)
        {
            playerFollowCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
            zoomVignette.SetActive(true);
        }
        else 
        {
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
            weaponCamera.fieldOfView = defaultFOV;
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
            zoomVignette.SetActive(false);
        }
    }
}
