using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Item : ScriptableObject
{
    public TextMeshProUGUI coinNumber;
    public int coinAmount = 0;
    public string itemName = "Default Item";
    public Sprite icon;
    public bool canShowInInventory = true;


    private void Awake()
    {
        coinAmount = 0;
    }
    public virtual void Use()
    {
        Debug.Log($"Using item : {itemName}");
    }

    public void RemoveFromInventory()
    {
        Inventory.Instance.Remove(this);
    }
}
