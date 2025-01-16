using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 3;
    
    [Header("Destroy VFX")]
    [SerializeField] GameObject destroyVFX;

    int currentHealth;

    void Awake() 
    {
        // Initialising starting health
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reducing Health By Damage Input

        if (currentHealth <= 0)
        {
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        Instantiate(destroyVFX, transform.position, Quaternion.identity);
        // Destroy GameObject if health is equal or below 0
        Destroy(this.gameObject);   
    }
}
