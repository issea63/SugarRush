using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FriendStats : CharacterStats
{
    public Transform objectSpawnPos;
    public GameObject objectSpawn;
    public GameObject canvas;
    
    public bool aimargay = false;
    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
        healthBar = GetComponentInChildren<HealthBarEnemyFinder>().gameObject.GetComponent<Image>();
        canvas = GetComponentInChildren<BillboardCanvas>().gameObject;
        healthBar.fillAmount = 1;
        canvas.SetActive(false);
    }
    public override void Die()
    {
        base.Die();
        Instantiate(objectSpawn, objectSpawnPos.position, Quaternion.identity);
        Debug.Log("Soy gay");
        aimargay = true;
    }
    public override void SetHealth(int amount)
    {
        base.SetHealth(amount);
        canvas.SetActive(true);
        FriendController controller = gameObject.GetComponent<FriendController>();
        controller.lookRadius = 1000f;
    }
}
