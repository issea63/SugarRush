using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PowerDoubleDamage : MonoBehaviour
{
    public Image reloadImage;
    public ManaManager mana;
    public float coolDown = 15f;
    public PlayerMotor motor;
    public LayerMask interactLayer;
    Animator animator;
    CharacterStats stats;
    private void Start()
    {
        mana = GetComponent<ManaManager>();
        motor = GetComponent<PlayerMotor>();
        animator = GetComponent<Animator>();
        stats = GetComponent<CharacterStats>();
        if (reloadImage == null)
        {
            GameObject imageObject = GameObject.FindWithTag("Image4");
            if (imageObject != null)
            {
                reloadImage = imageObject.GetComponent<Image>();
            }
        }
        coolDown = 0;
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            OnDoubleAttack(default);
        }

        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }
        else
        {
            coolDown = 0;
        }

        if (reloadImage != null)
        {
            reloadImage.fillAmount = coolDown / 15;
        }
    }

    public void OnDoubleAttack(InputValue value)
    {
        if (mana == null || motor == null || stats == null || Camera.main == null || Mouse.current == null ||
            coolDown > 0 || mana.mana < 25)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 100, interactLayer))
        {
            CharacterStats targetStats = raycastHit.collider.GetComponent<CharacterStats>();

            if (targetStats == null || raycastHit.collider.GetComponent<FriendStats>() != null) return;
            motor.moveOn = true;
            motor.FollowTarget(raycastHit.collider.GetComponent<Interactable>());
            motor.MoveToPoint(targetStats.transform.position);
            StartCoroutine(AttackRoutine(targetStats));

            coolDown = 15f;
            mana.UseMana(25);
        }
    }
    public IEnumerator AttackRoutine(CharacterStats targetStats)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            yield break;
        }

        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        if (targetStats != null)
        {
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }

            yield return new WaitForSeconds(.8f);
            targetStats.SetHealth(stats.damage.GetValue() * 2);
        }
    }
}
