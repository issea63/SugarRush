using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager Instance;

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

    public bool object1Active = false;
    public bool object2Active = false;
    public bool object3Active = false;

    public GameObject parent;
    public GameObject[] gameObjectsToActivate = new GameObject[3];

    Equipment[] currentEquipment;
    Inventory inventory;

    public delegate void OnEquipmentChanged(Equipment newEquipment, Equipment oldEquipment);
    public OnEquipmentChanged onEquipmentChangedCallback;

    private void Start()
    {
        inventory = Inventory.Instance;

        int numSlots = Enum.GetNames(typeof(Equipment.EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];

        FindObjectsAndApplyState();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindObjectsAndApplyState();
    }

    private void FindObjectsAndApplyState()
    {
        parent = GameObject.Find("Starter_P");

        if (parent == null)
        {
            Debug.LogWarning("No se encontró Starter_P en la escena.");
            return;
        }

        gameObjectsToActivate = new GameObject[3];

        foreach (var t in parent.GetComponentsInChildren<Transform>(true))
        {
            switch (t.name)
            {
                case "Starter_Helmet_P":
                    gameObjectsToActivate[0] = t.gameObject;
                    break;

                case "Starter_Boots_P":
                    gameObjectsToActivate[1] = t.gameObject;
                    break;

                case "Starter_Shoulders_P":
                    gameObjectsToActivate[2] = t.gameObject;
                    break;
            }
        }

        ApplySavedState();
    }

    private void ApplySavedState()
    {
        SetObjectActive(0, object1Active);
        SetObjectActive(1, object2Active);
        SetObjectActive(2, object3Active);
    }

    private void SetObjectActive(int index, bool active)
    {
        if (gameObjectsToActivate != null &&
            index < gameObjectsToActivate.Length &&
            gameObjectsToActivate[index] != null)
        {
            gameObjectsToActivate[index].SetActive(active);
        }
    }

    public void Equip(Equipment newEquipment)
    {
        int slotIndex = (int)newEquipment.slot;
        Equipment oldEquipment = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldEquipment = currentEquipment[slotIndex];
            inventory.Add(oldEquipment);
        }

        currentEquipment[slotIndex] = newEquipment;
        onEquipmentChangedCallback?.Invoke(newEquipment, oldEquipment);

        if (slotIndex == 0)
        {
            object1Active = true;
            SetObjectActive(0, true);
        }
        else if (slotIndex == 1)
        {
            object2Active = true;
            SetObjectActive(1, true);
        }
        else if (slotIndex == 2)
        {
            object3Active = true;
            SetObjectActive(2, true);
        }
    }
}
