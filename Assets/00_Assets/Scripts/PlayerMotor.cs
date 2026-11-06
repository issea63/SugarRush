using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    public bool moveOn = false;
    public bool rotationCorrect;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if(target != null)
        {
            FaceTarget();
        }
        if(moveOn && target != null)
        {
            FaceTarget();
            agent.SetDestination(target.position);
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        target = newTarget.interactionTransform;
        agent.stoppingDistance = newTarget.radius * 0.8f;
        agent.updateRotation = false;
    }

    public void StopFollowTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }
    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        if (Quaternion.Angle(transform.rotation, lookRotation) < 2f)
        {
            rotationCorrect = true;
        }
        else rotationCorrect = false;
    }
}
