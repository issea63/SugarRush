using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PowerMultiShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform[] originsMultiple;
    public float coolDown = 5f;
    public Image reloadImage;
    public ManaManager mana;
    public PlayerMotor motor;
    public LayerMask interactLayer;
    public AudioSource powerSource;
    public bool powerRepeat;
    private void Start()
    {
        mana = GetComponent<ManaManager>();
        motor = GetComponent<PlayerMotor>();
        powerSource = GameObject.Find("ShootSound").GetComponent<AudioSource>();
        if (reloadImage == null)
        {
            GameObject imageObject = GameObject.FindWithTag("Image2");
            if (imageObject != null)
            {
                reloadImage = imageObject.GetComponent<Image>();
            }
        }
        coolDown = 0;
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            OnMultiShot(default);
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
            reloadImage.fillAmount = coolDown / 5;
        }
    }

    public void OnMultiShot(InputValue value)
    {
        if (mana == null || motor == null || bulletPrefab == null || originsMultiple == null ||
            Camera.main == null || Mouse.current == null || coolDown > 0 || mana.mana < 15)
        {
            return;
        }

        motor.moveOn = false;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 100f, interactLayer))
        {
            powerRepeat = false;
            StartCoroutine(MultiShotRoutine(raycastHit));
        }
    }
    public IEnumerator MultiShotRoutine(RaycastHit ray)
    {
        motor.target = ray.collider.transform;

        motor.rotationCorrect = false;
        while (!motor.rotationCorrect)
        {
            yield return null;
        }

        if (!powerRepeat)
        {
            foreach (Transform origin in originsMultiple)
            {
                Instantiate(bulletPrefab, origin.position, origin.rotation);
                mana.UseMana(15);
                coolDown = 5f;
                powerSource.pitch = Random.Range(1, 2.60f);
                powerSource.Play();
                powerRepeat = true;
            }
        }
    }

}
