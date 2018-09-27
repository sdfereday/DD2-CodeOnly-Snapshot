using UnityEngine;
using UnityEngine.AI;

public class NavMeshInstantRotate : MonoBehaviour
{
    private NavMeshAgent agent;

	private void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }
	
	private void LateUpdate ()
    {
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }
    }
}
