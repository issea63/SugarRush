using System.Collections;
using UnityEngine;

public class BossCombat : MonoBehaviour
{
    public Animator animator;
    public float attackCooldown = 5f;
    public float animation1 = 2f;
    public float animation2 = 2f;
    public float attackDuration = 3f;
    public float attackDuration2 = 3f;

    public int attackRange;
    public GameObject beamDanger;
    public GameObject beamAttack;
    public GameObject[] collidersAttack1;

    private void Start()
    {
        attackCooldown = 5f;
        //animator = GetComponent<Animator>();
        foreach (GameObject collider in collidersAttack1)
        {
            collider.SetActive(false);
        }
    }
    private void Update()
    {
        attackCooldown -= Time.deltaTime;
        if(attackCooldown <= 0)
        {
            attackRange = Random.Range(1, 3);
            StartCoroutine("AttackRoutine");
            attackCooldown = 5;
        }
    }

    public IEnumerator AttackRoutine()
    {
        switch (attackRange)
        {
            case 1:
                animator.SetBool("SetIdle", false);
                animator.SetTrigger("Attack1");
                yield return new WaitForSeconds(animation1);
                foreach (GameObject collider in collidersAttack1)
                {
                    collider.SetActive(true);
                }
                yield return new WaitForSeconds(attackDuration);
                foreach (GameObject collider in collidersAttack1)
                {
                    collider.SetActive(false);
                }
                animator.SetBool("SetIdle", true);
                break;
            case 2:
                if(BossManager.Instance.gameObject.GetComponent<BossStats>().secondPhase)
                {
                    Debug.Log("attack2");
                    animator.SetBool("SetIdle", false);
                    animator.SetTrigger("Attack2");
                    yield return new WaitForSeconds(animation2);
                    Instantiate(beamDanger, PlayerManager.Instance.gameObject.transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(1.5f);
                    Transform danger = GameObject.Find("BeamDanger(Clone)").transform;
                    Vector3 spawnPosition = danger.position + Vector3.up * 10f;
                    Instantiate(beamAttack, spawnPosition, beamAttack.transform.localRotation);
                    Destroy(GameObject.Find("BeamDanger(Clone)"));
                    yield return new WaitForSeconds(1.5f);
                    Instantiate(beamDanger, PlayerManager.Instance.gameObject.transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(1.5f);
                    Transform danger2 = GameObject.Find("BeamDanger(Clone)").transform;
                    Vector3 spawnPosition2 = danger2.position + Vector3.up * 10f;
                    Instantiate(beamAttack, spawnPosition2, beamAttack.transform.localRotation);
                    Destroy(GameObject.Find("BeamDanger(Clone)"));
                    animator.SetTrigger("Attack2");
                    animator.SetBool("SetIdle", true);
                }
                else
                {
                    animator.SetBool("SetIdle", false);
                    animator.SetTrigger("Attack1");
                    yield return new WaitForSeconds(animation1);
                    foreach (GameObject collider in collidersAttack1)
                    {
                        collider.SetActive(true);
                    }
                    yield return new WaitForSeconds(attackDuration);
                    foreach (GameObject collider in collidersAttack1)
                    {
                        collider.SetActive(false);
                    }
                    animator.SetBool("SetIdle", true);
                    animator.SetBool("SetIdle", true);
                }
                break;
        }
        yield return null;
    }

}
