using Assets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum KeyAction { ZERO, ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, LAST}

public static class KeySetting { public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>(); }

public class QuickSlotManager : MonoBehaviour
{
    KeyCode[] DEFAULTKEYS = new KeyCode[] { KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };

    public Slot[] slots;
    public GameObject SlotParent;

    private PlayerClass ps;

    Event keyevent;
    int GETKEY = 10;


    private void Awake()
    {
        keyevent= new Event();
        for (int i = 0; i < (int)KeyAction.LAST; i++)
        {
            KeySetting.keys.Add((KeyAction)i, DEFAULTKEYS[i]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().player;
        slots = SlotParent.GetComponentsInChildren<Slot>();

        for (int i = 0; i < (int)KeyAction.LAST; i++)
        {
            slots[i].SetKeyCode(DEFAULTKEYS[i]);
        }
    }

    private void OnGUI()
    {
        //keyevent = Event.current;
        //if (keyevent.isKey)
        //{
        //    KeySetting.keys[0] = keyevent.keyCode;
        //    Debug.Log(keyevent.keyCode);
        //}
    }

    void Useditem(int key)
    {
        if (slots[key].item != null)
        {
            if (slots[key].item.itemType == Item.ItemType.Used)
            {
                if (slots[key].item is UsedItem v)
                {
                    Debug.Log(slots[key].item + "아이템 사용");
                    ps.Healing(v.HealHP, v.HealMP);
                    slots[key].AddCount(-1);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.ZERO]))
        {
            GETKEY = (int)KeyAction.ZERO;
            if (slots[GETKEY].skill != null)
            {
                //GameObject go = Instantiate(slots[GETKEY].SM.skills[0]);
                //go.SetActive(true);
                //slots[GETKEY].SkillUse();
            }
            else if (slots[GETKEY].item != null)
            {
                Useditem(GETKEY);
            }
        }
        else if (Input.GetKeyDown(KeySetting.keys[KeyAction.ONE]))
        {
            GETKEY = (int)KeyAction.ONE;
            if (slots[GETKEY].skill != null)
            {
                //GameObject go = Instantiate(slots[GETKEY].SM.skills[0]);
                slots[GETKEY].SkillUse();
                slots[GETKEY].SM.skills[0].GetComponent<BlackJack>().BlackJackPressed();
                //go.SetActive(true);
                //go.GetComponent<BlackJack>().BlackJackPressed();
            }
            else if (slots[GETKEY].item != null)
            {
                Useditem(GETKEY);
            }
        }
        else if (Input.GetKeyDown(KeySetting.keys[KeyAction.TWO]))
        {
            GETKEY = (int)KeyAction.TWO;
            Useditem(GETKEY);
        }
        else if (Input.GetKeyDown(KeySetting.keys[KeyAction.THREE]))
        {
            GETKEY = (int)KeyAction.THREE;
            Useditem(GETKEY);
        }
        else if (Input.GetKeyDown(KeySetting.keys[KeyAction.FOUR]))
        {
            GETKEY = (int)KeyAction.FOUR;
            Useditem(GETKEY);
        }
        else if (Input.GetKeyDown(KeySetting.keys[KeyAction.FIVE]))
        {
            GETKEY = (int)KeyAction.FIVE;
            Useditem(GETKEY);
        }
        else if (Input.GetKeyDown(KeySetting.keys[KeyAction.SIX]))
        {
            GETKEY = (int)KeyAction.SIX;
            Useditem(GETKEY);
        }
        else if (Input.GetKeyDown(KeySetting.keys[KeyAction.SEVEN]))
        {
            GETKEY = (int)KeyAction.SEVEN;
            Useditem(GETKEY);
        }
        else if (Input.GetKeyDown(KeySetting.keys[KeyAction.EIGHT]))
        {
            GETKEY = (int)KeyAction.EIGHT;
            Useditem(GETKEY);
        }
        else if (Input.GetKeyDown(KeySetting.keys[KeyAction.NINE]))
        {
            GETKEY = (int)KeyAction.NINE;
            Useditem(GETKEY);
        }

    }
}
