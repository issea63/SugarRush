using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    public GameObject player;
    #endregion
}