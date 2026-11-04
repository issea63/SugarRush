using UnityEngine;
using UnityEngine.AI;

public class FriendController : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    CharacterCombat characterCombat;
    public EnemyController enemyController;
    public LayerMask friendLayer;
    [SerializeField] private float detectionRadius = 10f;


    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
        Destroy(enemyController);
        if(BossManager.Instance != null)
        {
            target = BossManager.Instance.boss.transform;
        }
        else
        {
            target = PlayerManager.Instance.player.transform;
        }
        gameObject.GetComponent<FriendStats>().SetHealth(100);
        agent = GetComponent<NavMeshAgent>();
        characterCombat = GetComponent<CharacterCombat>();
    }

    private void Update()
    {
        FindEnemy();
        float distance = Vector3.Distance(target.position, transform.position);
        if(BossManager.Instance != null)
        {
            if (target == BossManager.Instance.boss.transform)
            {
                agent.speed = 5f;
                agent.SetDestination(target.position);

                if (distance <= agent.stoppingDistance && target == BossManager.Instance.boss.transform)
                {
                    CharacterStats ChStats = target.GetComponent<CharacterStats>();
                    characterCombat.Attack(ChStats);
                }
            }
        }
        if (enemyController != null)
        {
            agent.speed = 5f;
            Transform enemy = enemyController.transform;
            float distanceEnemy = Vector3.Distance(enemy.position, transform.position);

            agent.SetDestination(enemy.position);

            if (distanceEnemy <= agent.stoppingDistance)
            {
                EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();

                if (enemyStats != null)
                {
                    characterCombat.Attack(enemyStats);
                }
            }
        }
        else
        {
            agent.speed = 2.5f;
            agent.SetDestination(target.position);
        }
    }
    public void ReloadTarget()
    {
        if(BossManager.Instance !=null)
        {
            target = BossManager.Instance.boss.transform;
        }
    }

    void FindEnemy()
    {
        enemyController = null;

        EnemyController[] enemies = FindObjectsByType<EnemyController>();

        float radiusSqr = detectionRadius * detectionRadius;
        float closestDistanceSqr = radiusSqr;

        foreach (EnemyController enemy in enemies)
        {
            if (enemy == null) continue;

            float distanceSqr =
                (enemy.transform.position - transform.position).sqrMagnitude;

            if (distanceSqr <= closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                enemyController = enemy;
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere (transform.position, detectionRadius);
        
    }

}
