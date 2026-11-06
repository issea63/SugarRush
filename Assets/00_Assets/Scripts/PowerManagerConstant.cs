using UnityEngine;

public class PowerManagerConstant : MonoBehaviour
{
    #region Singleton

    public static PowerManagerConstant Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    #endregion

    public PowersManager powersManager;

    private void Update()
    {
        if (powersManager != null) PlayerManager.Instance.GetComponent<PowersManager>();
    }
}
