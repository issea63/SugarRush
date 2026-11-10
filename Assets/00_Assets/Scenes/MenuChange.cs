using UnityEngine;

public class MenuChange : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    { 
        Application.Quit();
    }

    private void Start()
    {
        if(FriendManager.Instance != null)
        {
            Destroy(FriendManager.Instance.gameObject);
        }
        if(Inventory.Instance != null)
        {
            Destroy(Inventory.Instance.gameObject);
        }
        if(Menu.Instance != null)
        {
            Destroy(Menu.Instance.gameObject);
        }
        if(EquipmentManager.Instance != null)
        {
            Destroy(EquipmentManager.Instance.gameObject);
        }
    }
}
