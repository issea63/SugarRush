using UnityEngine;

public class EnemyStats : CharacterStats
{
    public Transform objectSpawnPos;
    public GameObject objectSpawn;

    public bool aimargay = false;
    public override void Die()
    {
        base.Die();
        Instantiate(objectSpawn,objectSpawnPos.position, Quaternion.identity);
        Debug.Log("Soy gay");
        aimargay = true;
    }

}
