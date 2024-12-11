using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    FirstPersonController player;
    NavMeshAgent nmAgent;

    void Awake() 
    {
        nmAgent = GetComponent<NavMeshAgent>();
    }

    void Start() 
    {
        player = FindFirstObjectByType<FirstPersonController>();
    }

    void Update() 
    {
        nmAgent.SetDestination(player.transform.position);
    }
}
