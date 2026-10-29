using System;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    Equipment[] currentEquipment;
    Inventory inventory;
    public GameObject[] gameObjectsToActivate;
    public delegate void OnEquipmentChanged(Equipment newEquipment,  Equipment oldEquipment);
    public OnEquipmentChanged onEquipmentChangedCallback;
    private void Start()
    {
        inventory = Inventory.Instance;
        int numSlots = Enum.GetNames(typeof(Equipment.EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }
    public void Equip(Equipment newEquipment)
    {
        int slotIndex = (int)newEquipment.slot;
        Equipment oldEquipment = null;
        if(currentEquipment[slotIndex] != null)
        {
            oldEquipment = currentEquipment[slotIndex];
            inventory.Add(oldEquipment);
        }

        onEquipmentChangedCallback?.Invoke(newEquipment, oldEquipment);
        currentEquipment[slotIndex] = newEquipment;
    }
}