using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menuUI;

    private void Start()
    {
        Time.timeScale = 1f;
        if (menuUI != null)
        {
            menuUI.SetActive(false);
        }
    }
    public void OnMenu()
    {
        if (menuUI != null)
        {
            menuUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
    public void Resume()
    {
        if (menuUI != null)
        {
            Time.timeScale = 1f;
            menuUI.SetActive(false);
        }
    }
}
