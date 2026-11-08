
using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Movement")]
    public bool moveOn = false;
    public bool rotationCorrect = false;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("PlayerMotor: No se encuentra NavMeshAgent.", this);
        }
    }

    private void Update()
    {
        if (agent == null || !agent.enabled || !agent.isOnNavMesh)
            return;

        if (target != null)
        {
            FaceTarget();
        }
        else
        {
            rotationCorrect = false;
        }

        if (moveOn && target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        if (agent == null || !agent.enabled || !agent.isOnNavMesh)
            return;

        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        if (newTarget == null || newTarget.interactionTransform == null)
            return;

        target = newTarget.interactionTransform;

        if (agent != null && agent.enabled && agent.isOnNavMesh)
        {
            agent.stoppingDistance = newTarget.radius * 0.8f;
            agent.updateRotation = false;
        }
    }

    public void StopFollowTarget()
    {
        if (agent != null && agent.enabled)
        {
            agent.stoppingDistance = 0f;
            agent.updateRotation = true;
        }

        target = null;
        rotationCorrect = false;
    }

    public void FaceTarget()
    {
        if (target == null)
        {
            rotationCorrect = false;
            return;
        }

        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.0001f)
        {
            rotationCorrect = true;
            return;
        }

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            lookRotation,
            Time.deltaTime * 5f
        );

        rotationCorrect = Quaternion.Angle(
            transform.rotation,
            lookRotation
        ) < 2f;
    }
}
