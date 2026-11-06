using UnityEngine;

public class BossStats : CharacterStats
{
    public bool secondPhase = false;
    
    

    private void Start()
    {
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