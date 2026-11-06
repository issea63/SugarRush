using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    #region Singleton

    public static InventoryUI Instance;

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
    InventorySlot[] slots;
    public GameObject inventoryUI;

    private void Start()
    {
        inventoryUI = GameObject.FindWithTag("Inventory");
        Inventory.Instance.onItemChangedCallback += UpdateUI;
        slots = inventoryUI.GetComponentsInChildren<InventorySlot>();
    }

    public void OnInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(i < Inventory.Instance.items.Count)
            {
                slots[i].AddItem(Inventory.Instance.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}