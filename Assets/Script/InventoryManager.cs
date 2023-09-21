using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static bool InventoryActivated = false;

    private GameObject InventoryBase;

    public Slot[] slots;
    public GameObject SlotParent;

    // Start is called before the first frame update
    void Start()
    {
       
        slots = SlotParent.GetComponentsInChildren<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InputItem(Item item, int Count = 1)
    {
        if(Item.ItemType.Equipment != item.itemType)
        {
            for(int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.ItemName == item.ItemName)
                    {
                        slots[i].AddCount(Count);
                        return;
                    }
                }
            }
        }

        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].Additem(item,Count);
                return;
            }
        }
    }

}
