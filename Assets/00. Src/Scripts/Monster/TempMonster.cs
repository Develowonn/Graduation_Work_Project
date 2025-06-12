using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class TempMonster : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        agent.enabled = true;
    }

    public void InitPlayer(Transform player)
    {
        target = player;
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 targetPos = target.position;
            targetPos.y = transform.position.y; // Y√‡ ∞Ì¡§
            agent.SetDestination(targetPos);
        }
    }

    private void OnDisable()
    {
        agent.enabled = false;
    }
}
