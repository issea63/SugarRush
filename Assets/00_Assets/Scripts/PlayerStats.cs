using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats
{
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI armorText;
    private void Awake()
    {
        currentHealth = maxHealth;
        damageText = GameObject.FindWithTag("Damage").GetComponent<TextMeshProUGUI>();
        armorText = GameObject.FindWithTag("Armor").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        if(EquipmentManager.Instance.object1Active)
        {
            armor.AddModifier(-10);
        }
        if (EquipmentManager.Instance.object2Active)
        {
            armor.AddModifier(-5);
        }
        if (EquipmentManager.Instance.object3Active)
        {
            armor.AddModifier(-5);
        }

        UpdateText();
        EquipmentManager.Instance.onEquipmentChangedCallback += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newEquipment, Equipment oldEquipment)
    {
        armor.AddModifier(newEquipment.armorMod);
        damage.AddModifier(newEquipment.damageMod);
        if(oldEquipment != null)
        {
            armor.RemoveModifier(newEquipment.armorMod);
            damage.RemoveModifier(newEquipment.damageMod);
        }
        UpdateText();
    }

    public override void Die()
    {
        base.Die();

        SceneManager.LoadScene("LoseScene");
    }

    private void UpdateText()
    {
        damageText.text = "Damage: " + damage.GetValue() * -1;
        armorText.text ="Armor: " + armor.GetValue() * -1;
    }
}