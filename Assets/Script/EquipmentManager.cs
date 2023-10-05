using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private InventoryManager inventoryManager;
    public Slot[] slots;
    public GameObject SlotParent;
    private bool m_ISInit;
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init()
    {
        if (m_ISInit)
            return;

        m_ISInit = true;
        slots = SlotParent.GetComponentsInChildren<Slot>();
        inventoryManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().inventoryManager;
    }

    public void InputItem(Item item, int Count = 1)
    {
        Debug.Log("장착할 아이템 : " + item.ItemName);
        if (Item.ItemType.Equipment == item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (item is EquipmentItem v)
                {
                    Debug.Log(v.equipmenttype);
                    if (slots[i].name == (Enum.GetName(typeof(EquipmentItem.EquipmentType), v.equipmenttype)))
                    {
                        if (slots[i].item == null)
                        {
                        }
                        else
                        {
                            inventoryManager.InputItem(slots[i].item);
                            slots[i].item = null;
                        }
                        slots[i].Additem(item, Count);
                        return;
                    }
                }
            }
        }
    }
}
