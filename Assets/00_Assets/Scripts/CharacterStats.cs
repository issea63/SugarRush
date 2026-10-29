using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 100;
    public int currentHealth {get; private set;}
    public Stats armor;
    public Stats damage;

    [Header("Objects")]
    public Image healthBar;

    [Header("Other")]
    public Animator animator;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public void SetHealth(int amount)
    {
        amount -= armor.GetValue();
         currentHealth += amount;
        if(currentHealth >= maxHealth) {currentHealth = maxHealth;}
        UpdateHealthUI();
        if (currentHealth <= 0) { Die(); }
    }
    public virtual void Die()
    {
        StartCoroutine("DieRoutine");
    }
    public void UpdateHealthUI()
    {
        healthBar.fillAmount = (float) currentHealth / maxHealth;
    }

    public IEnumerator DieRoutine()
    {
        //animator.SetTrigger("Die");
        //yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        yield return null;
    }
}