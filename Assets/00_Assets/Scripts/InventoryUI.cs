using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    InventorySlot[] slots;
    public GameObject inventoryUI;

    private void Start()
    {
        Inventory.Instance.onItemChangedCallback += UpdateUI;
        slots = inventoryUI.GetComponentsInChildren<InventorySlot>();
    }

    void OnInventory()
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