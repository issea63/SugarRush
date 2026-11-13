using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PowerHeal : MonoBehaviour
{
    public Image reloadImage;
    public ManaManager mana;
    public float coolDown = 15f;

    private void Start()
    {
        mana = GetComponent<ManaManager>();
        if (reloadImage == null)
        {
            GameObject imageObject = GameObject.FindWithTag("Image5");
            if (imageObject != null)
            {
                reloadImage = imageObject.GetComponent<Image>();
            }
        }
        coolDown = 0;
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.digit5Key.wasPressedThisFrame)
        {
            OnHeal(default);
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

    public void OnHeal(InputValue value)
    {
        PlayerStats stats = GetComponent<PlayerStats>();
        if (mana == null || stats == null || coolDown > 0 || mana.mana < 50)
        {
            return;
        }

        stats.SetHealth(30);
        coolDown = 15f;
        mana.UseMana(50);
    }
}
