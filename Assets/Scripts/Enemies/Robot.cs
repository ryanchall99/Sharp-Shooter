using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    FirstPersonController player;
    NavMeshAgent nmAgent;
    EnemyHealth enemyHealth;

    const string PLAYER_STRING = "Player";

    void Awake() 
    {
        nmAgent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Start() 
    {
        player = FindFirstObjectByType<FirstPersonController>();
    }

    void Update() 
    {
        nmAgent.SetDestination(player.transform.position);
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            enemyHealth.SelfDestruct();
        }
    }
}
