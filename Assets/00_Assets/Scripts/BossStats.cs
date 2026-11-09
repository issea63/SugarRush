using UnityEngine;
using UnityEngine.UI;

public class BossStats : CharacterStats
{
    public bool secondPhase = false;
    
    

    private void Start()
    {
        healthBar = GameObject.FindWithTag("BossBar").GetComponent<Image>();
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
}