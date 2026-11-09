using UnityEngine;

public class BossManager : MonoBehaviour
{
    #region Singleton
    public static BossManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    public GameObject boss;
    #endregion
    private void Start()
    {
        BossCombat combat = GetComponent<BossCombat>();
        Boss boss = GetComponent<Boss>();
        BossStats stats = GetComponent<BossStats>();
        combat.enabled = true;
        boss.enabled = true;
        stats.enabled = true;
    }
}
