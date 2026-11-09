using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    bool power1;
    bool power2;
    public bool powerUp2;
    public bool powerUp3;
    public bool powerUp4;
    public bool powerUp5;
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Image image5;
    [Header ("Others")]
    CharacterStats stats;
    public Animator animator;
    public LayerMask interactLayer;
    PlayerMotor motor;
    public Image manaBottle;
    
    #region Singleton
    public static PowersManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    private void Start()
    {
        image1 = GameObject.FindWithTag("Image1").GetComponent<Image>();
        image2 = GameObject.FindWithTag("Image2").GetComponent<Image>();
        image3 = GameObject.FindWithTag("Image3").GetComponent<Image>();
        image4 = GameObject.FindWithTag("Image4").GetComponent<Image>();
        image5 = GameObject.FindWithTag("Image5").GetComponent<Image>();
        manaBottle = GameObject.FindWithTag("Mana").GetComponent<Image>();
        motor = GetComponent<PlayerMotor>();
        stats = GetComponent<CharacterStats>();
        animator = GetComponentInChildren<Animator>();
        multiShotCooldown = 0;
        shotCooldown = 0;
        healCooldown = 0;
        dashCooldown = 0;
        attackCooldown = 0;
        mana = 100f;
    }
    private void Update()
    {
        if (mana >= 100) { mana = 100; }
        if (mana <= 100) { mana += Time.deltaTime; }
        if (shotCooldown >= 0) { shotCooldown -= Time.deltaTime; image1.fillAmount = shotCooldown / 1; } else { shotCooldown = 0; }
        if (multiShotCooldown >= 0) { multiShotCooldown -= Time.deltaTime; image2.fillAmount = multiShotCooldown / 5f; } else { multiShotCooldown = 0; }
        if (healCooldown >= 0) { healCooldown -= Time.deltaTime; image5.fillAmount = healCooldown / 20; } else { healCooldown = 0; }
        if (dashCooldown >= 0) { dashCooldown -= Time.deltaTime; image3.fillAmount = dashCooldown / 2; } else { dashCooldown = 0; }
        if (dashActive) { dashDuration -= Time.deltaTime; } 
        if (dashDuration <= 0) { dashActive = false; gameObject.GetComponent<NavMeshAgent>().speed = 3.5f; dashDuration = 2f; }
        if (attackCooldown >= 0) { attackCooldown -= Time.deltaTime; image4.fillAmount = attackCooldown / 15; } else { attackCooldown = 0; }
        ManaUpdate();
    }

    public void OnShoot()
    {
        if (shotCooldown <= 0 && mana >= 5 && powerUp1)
        {
            motor.moveOn = false;
            if (EventSystem.current.IsPointerOverGameObject()) return;

            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, 100, interactLayer))
            {
                power1 = false;
                StartCoroutine("ShootRoutine", raycastHit);
            }
            else return;
        }
        else return;
    }
    public IEnumerator ShootRoutine(RaycastHit ray)
    {
        animator.SetTrigger("Spell");
        motor.target = ray.collider.transform;
        motor.rotationCorrect = false;

        while (!motor.rotationCorrect)
        {
            yield return null;
        }
        if (!power1)
        {
            Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation);
            mana -= 5;
            shotCooldown = 1f;
            power1 = true;
        }
    }

    public void OnMultiShot()
    {
        if (multiShotCooldown <= 0 && mana >= 15 && powerUp2)
        {
            motor.moveOn = false;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, interactLayer))
            {
                power2 = false;
                StartCoroutine("MultiShotRoutine",hit );
            }
        }
        else return;
    }
    public IEnumerator MultiShotRoutine(RaycastHit ray)
    {
        animator.SetTrigger("Spell");
        motor.target = ray.collider.transform;

        motor.rotationCorrect =false;
        while (!motor.rotationCorrect)
        {
            yield return null;
        }        

        if(!power2)
        {
            foreach (Transform origin in originsMultiple)
            {
                Instantiate(bullet, origin.position, origin.rotation);
                mana -= 5f;
                power2 = true;
            }
        }

            multiShotCooldown = 5f;
    }
    public void OnHeal()
    {
        if (healCooldown <= 0 && mana >= 50 && powerUp3)
        {
            animator.SetTrigger("Heal");
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
                CharacterStats targetStats = raycastHit.collider.GetComponent<CharacterStats>();

                if (targetStats == null || raycastHit.collider.GetComponent<FriendStats>() ) return;
                motor.moveOn = true;
                motor.FollowTarget(raycastHit.collider.GetComponent<Interactable>());
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
    public void ManaUpdate()
    {
        manaBottle.fillAmount = mana / 100f;
    }


}
