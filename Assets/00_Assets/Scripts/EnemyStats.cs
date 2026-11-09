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
        //Instantiate(objectSpawn,objectSpawnPos.position, Quaternion.identity);
        gameObject.GetComponent<Enemy>().enabled = false;
        gameObject.GetComponent<EnemyStats>().enabled = false;
        gameObject.AddComponent<FriendController>().enabled = true;
        gameObject.AddComponent<FriendStats>().enabled = true;
        FriendManager.Instance.UpdateFriends();
        gameObject.name = ("Friend");
        Debug.Log("Soy gay");
        aimargay = true;
    }
    public override void SetHealth(int amount)
    {
        base.SetHealth(amount);
        canvas.SetActive(true);
        EnemyController controller = gameObject.GetComponent<EnemyController>();
        controller.lookRadius = 1000f;
    }

}
