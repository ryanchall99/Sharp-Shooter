using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;
    [SerializeField] int damage = 3;

    void Start() 
    {
        Explode();
    }

    void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);    
    }

    void Explode()
    {
        // Do damage to player
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hitCollider in hitColliders)
        {
            PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();

            if (!playerHealth) continue; // If collider does not have PlayerHealth script continue to next collider in array

            playerHealth.TakeDamage(damage); // Take Damage

            break; // Break out of loop
        }        
    }
}
