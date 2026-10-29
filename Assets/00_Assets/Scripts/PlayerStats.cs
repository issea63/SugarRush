using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats
{
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI armorText;

    private void Start()
    {
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