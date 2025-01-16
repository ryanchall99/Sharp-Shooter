using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 5;
    
    int currentHealth;

    void Awake() 
    {
        // Initialising starting health
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reducing Health By Damage Input
        Debug.Log(damage + " damage taken!");

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
