using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Particle Systems")]
    [SerializeField] ParticleSystem muzzleFlash;

    [Header("Interactions")]
    [SerializeField] LayerMask interactionLayers;

    public void Shoot(WeaponSO weaponSO)
    {
        muzzleFlash.Play();
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore))
        {
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity); // Instantiates a hit VFX GameObject where the Raycast collides

            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

            // Only proceeds if enemyHealth is not null
            enemyHealth?.TakeDamage(weaponSO.Damage); // NOTE: ? = Null Conditional Operator
        }
    }
}
