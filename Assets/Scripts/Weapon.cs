using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Animator animator;

    [Header("Particle Systems")]
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitVFXPrefab; 

    // --- WEAPON STATS ---
    [Header("Weapon Stats")]
    [SerializeField] WeaponSO weaponSO;

    // --- PLAYER INPUT ---
    StarterAssetsInputs starterAssetsInputs;

    const string SHOOT_STRING = "Shoot";

    void Awake() 
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }

    void Update()
    {
        HandleShoot();
    }

    void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;

        muzzleFlash.Play();
        animator.Play(SHOOT_STRING, 0, 0f);

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            Instantiate(hitVFXPrefab, hit.point, Quaternion.identity); // Instantiates a hit VFX GameObject where the Raycast collides

            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

            // Only proceeds if enemyHealth is not null
            enemyHealth?.TakeDamage(weaponSO.Damage); // NOTE: ? = Null Conditional Operator
        }

        starterAssetsInputs.ShootInput(false);
    }
}
