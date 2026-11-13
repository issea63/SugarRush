using TMPro;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    private void Start()
    {
        player = PlayerManager.Instance.player.transform;
        gameObject.AddComponent<ItemRotate>();
        if (item.itemName == "BagCoins")
        {
            item.coinAmount = 0;
        }
    }

    public override void Interact()
    {
        base.Interact();
        pickupSound.Play();
        Debug.Log("Interacting with ItemPickup " + transform.name);
        //Pickup item, add to inventory, etc.
        Inventory.Instance.Add(item);
        Destroy(gameObject);
    }
}