using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PowerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletOrigin;

    public float coolDown = 1f;
    public Image reloadImage;
    public ManaManager mana;
    public PlayerMotor motor;
    public LayerMask interactLayer;
    public bool powerRepeat;
    public AudioSource powerSource;

    private void Start()
    {
        mana = GetComponent<ManaManager>();
        motor = GetComponent<PlayerMotor>();
        powerSource = GameObject.Find("ShootSound").GetComponent<AudioSource>();

        PlayerInput playerInput = GetComponent<PlayerInput>();
        if (playerInput != null && playerInput.actions != null)
        {
            playerInput.actions.FindActionMap("Player", true).Enable();
        }

        if (reloadImage == null)
        {
            GameObject imageObject = GameObject.FindWithTag("Image1");
            if (imageObject != null)
            {
                reloadImage = imageObject.GetComponent<Image>();
            }
        }

        coolDown = 0;
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            OnShoot(default);
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
            reloadImage.fillAmount = coolDown;
        }
    }

    public void OnShoot(InputValue value)
    {
        if (mana == null || motor == null || bulletPrefab == null || bulletOrigin == null ||
            mana.mana < 5 || coolDown > 0)
        {
            return;
        }

        if (Camera.main == null || Mouse.current == null)
        {
            return;
        }

        motor.moveOn = false;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 100, interactLayer))
        {
            powerRepeat = false;
            StartCoroutine(ShootRoutine(raycastHit));
        }
    }

    public IEnumerator ShootRoutine(RaycastHit ray)
    {
        motor.target = ray.collider.transform;
        motor.rotationCorrect = false;

        while (!motor.rotationCorrect)
        {
            yield return null;
        }
        if (!powerRepeat)
        {
            Instantiate(bulletPrefab, bulletOrigin.position, bulletOrigin.rotation);
            mana.UseMana(5);
            coolDown = 1f;
            powerSource.pitch = Random.Range(1, 2.60f);
            powerSource.Play();
            powerRepeat = true;
        }
    }
}
