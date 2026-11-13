using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PowerDash : MonoBehaviour
{
    public Image reloadImage;
    public ManaManager mana;
    public float coolDown = 3f;
    public float duration = 2f;
    public bool dashActive = false;
    private NavMeshAgent agent;

    private void Start()
    {
        mana = GetComponent<ManaManager>();
        agent = GetComponent<NavMeshAgent>();

        if (reloadImage == null)
        {
            GameObject imageObject = GameObject.FindWithTag("Image3");
            if (imageObject != null)
            {
                reloadImage = imageObject.GetComponent<Image>();
            }
        }

        coolDown = 0;
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            OnDash(default);
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
            reloadImage.fillAmount = coolDown / 3;
        }

        if (dashActive)
        {
            duration -= Time.deltaTime;
        }

        if (duration <= 0)
        {
            dashActive = false;
            if (agent != null)
            {
                agent.speed = 3.5f;
            }
            duration = 2f;
        }
    }

    public void OnDash(InputValue value)
    {
        if (agent == null || mana == null || coolDown > 0 || mana.mana < 5)
        {
            return;
        }

        agent.speed = 8f;
        dashActive = true;
        duration = 2f;
        coolDown = 3f;
        mana.UseMana(5);
    }
}
