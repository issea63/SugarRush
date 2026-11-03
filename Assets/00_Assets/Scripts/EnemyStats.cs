using UnityEngine;

public class EnemyStats : CharacterStats
{
    public Transform objectSpawnPos;
    public GameObject objectSpawn;
    public GameObject canvas;

    public bool aimargay = false;
    public override void Die()
    {
        base.Die();
        Instantiate(objectSpawn,objectSpawnPos.position, Quaternion.identity);
        Debug.Log("Soy gay");
        aimargay = true;
    }
    public override void SetHealth(int amount)
    {
        base.SetHealth(amount);
        canvas.SetActive(true);
    }

}
