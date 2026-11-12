using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
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
    public void Quit()
    {
        Application.Quit();
    }
}
