using UnityEngine;
using UnityEngine.AI;

public class CharacterAnim : MonoBehaviour
{
    float animationSmoothTime = .1f;
    NavMeshAgent agent;
    Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        float agentSpeed = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("Speed",agentSpeed, animationSmoothTime,Time.deltaTime);
    }
}
