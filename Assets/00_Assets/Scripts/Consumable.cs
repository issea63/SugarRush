using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item
{
    public override void Use()
    {
        base.Use();
        if (itemName == "BagCoins")
        {
            coinNumber = GameObject.FindGameObjectWithTag("CoinNumber").GetComponent<TextMeshProUGUI>();
            coinAmount += 100;
            coinNumber.text = "Coins: " + coinAmount;
            if (coinAmount >= 300)
            {
                SceneManager.LoadScene("WinScene");
            }
        }
        if (itemName == "Health")
        {
            if (PlayerManager.Instance.GetComponent<CharacterStats>().currentHealth >= PlayerManager.Instance.GetComponent<CharacterStats>().maxHealth)
            {
                return;
            }
            else
            {
                PlayerManager.Instance.GetComponent<CharacterStats>().SetHealth(20);
                PlayerManager.Instance.GetComponent<CharacterStats>().UpdateHealthUI();
            }
        }
        if(itemName == "ManaPotion")
        {
            if(PowersManager.Instance.mana >= 100)
            {
                PowersManager.Instance.mana = 100;
            }
            else
            {
                PowersManager.Instance.mana += 20;
            }
        }
        Debug.Log("Using consumable item");
        RemoveFromInventory();
    }
}
