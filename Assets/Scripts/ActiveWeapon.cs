using Cinemachine;
using StarterAssets;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    // --- WEAPON STATS ---
    [Header("Weapon Stats")]
    [SerializeField] WeaponSO weaponSO;

    [Header("Weapon Zoom")]
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] GameObject zoomVignette;

    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    FirstPersonController firstPersonController;
    Weapon currentWeapon;

    const string SHOOT_STRING = "Shoot";

    float timeSinceLastShot = 0f;
    float defaultFOV;
    float defaultRotationSpeed;

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
        currentWeapon = GetComponentInChildren<Weapon>();  
    }

    void Update()
    {
        HandleShoot();
        HandleZoom();
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
        timeSinceLastShot += Time.deltaTime; // Fire Rate Logic

        if (!starterAssetsInputs.shoot) return;

        if (timeSinceLastShot >= weaponSO.FireRate)
        {
            currentWeapon.Shoot(weaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            timeSinceLastShot = 0f; // Reset Timer
        }

        // If Weapon Is Not Automatic
        if (!weaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }
    }

    void HandleZoom()
    {
        if (!weaponSO.CanZoom) return; // Cannot Zoom In

        if (starterAssetsInputs.zoom)
        {
            playerFollowCamera.m_Lens.FieldOfView = weaponSO.ZoomAmount;
            firstPersonController.ChangeRotationSpeed(weaponSO.ZoomRotationSpeed);
            zoomVignette.SetActive(true);
        }
        else 
        {
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
            zoomVignette.SetActive(false);
        }
    }
}
