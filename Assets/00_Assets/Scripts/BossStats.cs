using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossStats : CharacterStats
{
    public bool secondPhase = false;

    public Image barBg;

    private void Start()
    {
        healthBar = GameObject.FindWithTag("BossBar").GetComponent<Image>();
        barBg = GameObject.FindWithTag("BarBg").GetComponent<Image>();
        barBg.enabled = true;
        healthBar.enabled = true;
        maxHealth = 350;
        currentHealth = maxHealth;
    }
    public override void SetHealth(int amount)
    {
        base.SetHealth(amount);
        if(currentHealth < maxHealth/2)
        {
            secondPhase = true;
        }
    }
    public override void Die()
    {
        base.Die();
        StartCoroutine("DieRoutineBoss");
    }
    public IEnumerator DieRoutineBoss()
    {
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("WinScene");
    }
}