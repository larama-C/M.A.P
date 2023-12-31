using Assets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private PlayerClass ps;

    [SerializeField] private TextMeshProUGUI[] Coin;
    public Slot[] slots;
    public GameObject SlotParent;
    public GameObject CoinParent;

    bool m_ISInit = false;
    public void Init()
    {
        if (m_ISInit)
        {
            return;
        }
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().player;
        slots = SlotParent.GetComponentsInChildren<Slot>();
        Coin = CoinParent.GetComponentsInChildren<TextMeshProUGUI>();
        m_ISInit = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_ISInit == true || Coin != null)
        {
            UPDATEUI();
        }
    }

    private void UPDATEUI()
    {
        Coin[0].text = ps.Meso.ToString();
        Coin[1].text = ps.MaplePoint.ToString();
        //Coin[0].text = GameManager.Instance.Meso.ToString();
        //Coin[1].text = GameManager.Instance.MaplePoint.ToString();
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
                Debug.Log("인벤토리 인");
                slots[i].Additem(item, Count);
                return;
            }
        }
    }

}
