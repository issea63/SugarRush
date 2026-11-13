using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BulletLogic : MonoBehaviour
{
    public AudioSource source;
    public float bulletSpeed = 2f;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f);
    }
    private void Update()
    {
        rb.linearVelocity = transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<CharacterStats>().SetHealth(-10);
            if(other.GetComponent<Enemy>() != null)
            {
                other.GetComponent<Enemy>().enemycanvas.SetActive(true);
                other.GetComponent<EnemyController>().lookRadius = 10000000f;
                source.Play();
                Destroy(gameObject);
            }
        }
    }
}
