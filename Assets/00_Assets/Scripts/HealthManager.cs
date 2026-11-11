using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public Image healthBar;

    public AnimationClip clip;
    public Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hit");
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void Die()
    {
        Debug.Log(transform.name + " has died.");
        if(transform.name == "Player")
        {
            animator.SetTrigger("Die");
            SceneManager.LoadScene("LoseScene");
        }
        // Add death logic here (e.g., play animation, disable character, etc.)
        Destroy(gameObject);
    }
}
