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
}
