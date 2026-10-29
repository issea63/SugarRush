using UnityEngine;
[CreateAssetMenu(fileName = "NewArmor", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public int damageMod;
    public int armorMod;
    public EquipmentSlot slot;
    public override void Use()
    {
        base.Use();
        //Equip
        EquipmentManager.Instance.Equip(this);
        //Erakutsi
        EquipmentManager.Instance.gameObjectsToActivate[(int)slot].SetActive(true);
        RemoveFromInventory();
    }
    public enum EquipmentSlot {Head, Shoulder,shoes}
}
