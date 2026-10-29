using System.Collections;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    public float damage = 10f;
    public float attackSpeed = 1f;
    float attackCooldown = 0f;
    public AnimationClip clip;
    public Animator animator;
    public CharacterStats stats;
    private void Start()
    {
        stats = GetComponent<CharacterStats>();
    }
    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= .5)
        {
            StartCoroutine("AttackRoutine", targetStats);
            attackCooldown = 2 / attackSpeed;
        }
    }

    public IEnumerator AttackRoutine(CharacterStats targetStats)
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(.8f);
        targetStats.SetHealth(stats.damage.GetValue());
        yield break;
    }
}
