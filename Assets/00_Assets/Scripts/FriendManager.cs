using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FriendManager : MonoBehaviour
{
    #region Singleton
    public static FriendManager Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public GameObject friendPrefab;
    public GameObject friendPrefab2;
    public GameObject friendPrefab3;
    public int randomRange;
    public Transform spawnPoint;
    public FriendController[] controllers;
    public int previousControllerCount;
    public void UpdateFriends()
    {
        controllers = FindObjectsByType<FriendController>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadScene(string sceneName)
    {
        previousControllerCount = FindObjectsByType<FriendController>().Length;
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");
        if (spawnPoint == null)
        {
            //Debug.LogError("Null");
            return;
        }

        for (int i = 0; i < previousControllerCount; i++)
        {
            int prefabIndex = Random.Range(0, 4);
            GameObject prefab = null;
            switch (prefabIndex)
            {
                case 0:
                    prefab = friendPrefab;
                    break;
                case 1:
                    prefab = friendPrefab2;
                    break;
                case 2:
                    prefab = friendPrefab3;
                    break;
            }
            Instantiate(prefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }
}
