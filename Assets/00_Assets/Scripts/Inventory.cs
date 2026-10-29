using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion


    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public int maxInventorySize = 20;
    public List<Item> items = new List<Item>();

    public void Add(Item item)
    {
        if(item.canShowInInventory)
        {
            if(items.Count >= maxInventorySize)
            {
                Debug.Log("Inventory is full!");
                return;
            }
            items.Add(item);
            onItemChangedCallback?.Invoke();
        }
    }
    public void Remove(Item item)
    {
        items.Remove(item);
        onItemChangedCallback?.Invoke();
    }
}
