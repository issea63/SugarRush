using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PowersManager : MonoBehaviour
{

    public GameObject bullet;

    public Transform bulletOrigin;

    public Transform[] originsMultiple;

    public float mana = 100f;

    public float shotCooldown = 0;
    public float multiShotCooldown = 5f;

    public LayerMask interactLayer;

    PlayerMotor motor;

    private void Start()
    {
        motor= GetComponent<PlayerMotor>();
        multiShotCooldown = 5f;
        shotCooldown = 1;
        mana = 100f;
    }
    private void Update()
    {
        if (mana >= 100) { mana = 100; }
        if (mana <= 100) { mana += Time.deltaTime; }
        if (shotCooldown >= 0) { shotCooldown -= Time.deltaTime; } else { shotCooldown = 0; }
        if (multiShotCooldown >= 0) { multiShotCooldown -= Time.deltaTime; } else {  multiShotCooldown = 0; }
    }

    public void OnShoot()
    {
        if (shotCooldown <= 0)
        {
            //if (EventSystem.current.IsPointerOverGameObject()) return;

            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit, 100, interactLayer))
            {
                motor.target = raycastHit.collider.transform;
                motor.FaceTarget();
                shotCooldown = 1;
                Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation);
                mana -= 5;
            }
        }
        else return;
    }

    public void OnMultiShot()
    {
        if (multiShotCooldown <= 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, interactLayer))
            {
                motor.target = hit.collider.transform;
                motor.FaceTarget();

                foreach (Transform origin in originsMultiple)
                {
                    Instantiate(bullet, origin.position, origin.rotation);
                    mana -= 10;
                }

                multiShotCooldown = 5f;
            }
        }
        else return;
    }
}
