using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
     public TextMeshProUGUI damageText;
     public TextMeshProUGUI armorText;
    private void Awake()
    {
        healthBar = GameObject.FindGameObjectWithTag("PlayerBar").GetComponent<Image>();
        damageText = GameObject.FindGameObjectWithTag("Damage").GetComponent<TextMeshProUGUI>();
        armorText = GameObject.FindGameObjectWithTag("Armor").GetComponent<TextMeshProUGUI>();
        currentHealth = maxHealth;
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
    private void Update()
    {
        if(damageText == null)
        {
            damageText = GameObject.FindWithTag("Damage").GetComponent<TextMeshProUGUI>();
        }
        if (armorText == null)
        {
            armorText = GameObject.FindWithTag("Armor").GetComponent<TextMeshProUGUI>();
        }
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
    public AudioSource die;
    public override void Die()
    {
        base.Die();
        StartCoroutine("DieRoutinePlayer");
    }
    public IEnumerator DieRoutinePlayer()
    {
        PlayerManager.Instance.GetComponent<PlayerController>().enabled = false;
        animator.SetTrigger("Die");
        die.Play();
        yield return new WaitForSeconds(1.5f);
        if(BossManager.Instance != null)
        {
            SceneManager.LoadScene("LoseSceneBoss");
        }
        else
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    private void UpdateText()
    {
        damageText.text = "Damage: " + damage.GetValue() * -1;
        armorText.text ="Armor: " + armor.GetValue() * -1;
    }
}