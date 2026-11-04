using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PowersManager : MonoBehaviour
{
    [Header("Objects")]
    public GameObject bullet;
    public Transform bulletOrigin;
    public Transform[] originsMultiple;
    [Header("Stats")]
    public float mana = 100f;
    float shotCooldown = 0;
    float multiShotCooldown = 5f;
    float healCooldown = 20f;
    float dashCooldown = 3f;
    float dashDuration = 2f;
    float attackCooldown = 15f;
    bool dashActive = false;
    public bool powerUp1;
    public bool powerUp2;
    public bool powerUp3;
    public bool powerUp4;
    public bool powerUp5;
    [Header ("Others")]
    CharacterStats stats;
    Animator animator;
    public LayerMask interactLayer;
    PlayerMotor motor;

    #region Singleton
    public static PowersManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        stats = GetComponent<CharacterStats>();
        animator = GetComponentInChildren<Animator>();
        multiShotCooldown = 0;
        shotCooldown = 0;
        healCooldown = 0;
        attackCooldown = 0;
        mana = 100f;
    }
    private void Update()
    {
        if (mana >= 100) { mana = 100; }
        if (mana <= 100) { mana += Time.deltaTime; }
        if (shotCooldown >= 0) { shotCooldown -= Time.deltaTime; } else { shotCooldown = 0; }
        if (multiShotCooldown >= 0) { multiShotCooldown -= Time.deltaTime; } else { multiShotCooldown = 0; }
        if (healCooldown >= 0) { healCooldown -= Time.deltaTime; } else { healCooldown = 0; }
        if (dashCooldown >= 0) { dashCooldown -= Time.deltaTime; } else { dashCooldown = 0; }
        if (dashActive) { dashDuration -= Time.deltaTime; } 
        if (dashDuration <= 0) { dashActive = false; gameObject.GetComponent<NavMeshAgent>().speed = 3.5f; dashDuration = 2f; }
        if (attackCooldown >= 0) { attackCooldown -= Time.deltaTime; } else { attackCooldown = 0; }
    }

    public void OnShoot()
    {
        if (shotCooldown <= 0 && mana >= 5 && powerUp1)
        {
            motor.moveOn = false;
            if (EventSystem.current.IsPointerOverGameObject()) return;

            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit, 100, interactLayer))
            {
                StartCoroutine("ShootRoutine", raycastHit);
            }
        }
        else return;
    }
    public IEnumerator ShootRoutine(RaycastHit ray)
    {
        //animator etc.
        motor.target = ray.collider.transform;
        motor.rotationCorrect = false;

        while (!motor.rotationCorrect)
        {
            yield return null;
        }

        Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation);
        mana -= 5;
        shotCooldown = 1f;
    }

    public void OnMultiShot()
    {
        if (multiShotCooldown <= 0 && mana >= 15 && powerUp2)
        {
            motor.moveOn = false;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, interactLayer))
            {
                StartCoroutine("MultiShotRoutine",hit );
            }
        }
        else return;
    }
    public IEnumerator MultiShotRoutine(RaycastHit ray)
    {
        //animator etc
        motor.target = ray.collider.transform;

        motor.rotationCorrect =false;
        while (!motor.rotationCorrect)
        {
            yield return null;
        }        
            foreach (Transform origin in originsMultiple)
            {
                Instantiate(bullet, origin.position, origin.rotation);
                mana -= 5f;
            }

            multiShotCooldown = 5f;
    }
    public void OnHeal()
    {
        if (healCooldown <= 0 && mana >= 50 && powerUp3)
        {
            gameObject.GetComponent<PlayerStats>().SetHealth(30);
            healCooldown = 20f;
            mana -= 50;
        }
    }
    public void OnDash()
    {
        if(dashCooldown <= 0 && mana >= 5 && !dashActive && powerUp4)
        {
            gameObject.GetComponent<NavMeshAgent>().speed = 8f;
            dashActive = true;
            dashDuration = 2f;
            dashCooldown = 4f;
            mana -= 5;
        }
    }
    public void OnDoubleAttack(InputValue value)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit raycastHit;

        if (attackCooldown <= 0 && mana >= 25 && powerUp5 )
        {
            if (Physics.Raycast(ray, out raycastHit, 100, interactLayer))
            {
                EnemyStats targetStats = raycastHit.collider.GetComponent<EnemyStats>();

                if (targetStats == null) return;
                motor.moveOn = true;
                motor.FollowTarget(raycastHit.collider.GetComponent<Enemy>());
                motor.MoveToPoint(targetStats.transform.position);
                StartCoroutine(AttackRoutine(targetStats));

                attackCooldown = 15f;
                mana -= 25;
            }
        }
    }
    public IEnumerator AttackRoutine(CharacterStats targetStats)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        if (targetStats != null)
        {
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(.8f);
            targetStats.SetHealth(stats.damage.GetValue() * 2);
        }
    }


}
