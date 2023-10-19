using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler, IDropHandler, IBeginDragHandler
{
    public Skill skill;
    public Item item;
    public int itemCount;

    public Image[] itemImage;
    private TextMeshProUGUI CountText;
    public TextMeshProUGUI KeyCode = null;
    private EquipmentManager EM;
    private InventoryManager IM;
    private PlayerClass ps;

    private bool SkillUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        itemImage = GetComponentsInChildren<Image>();
        CountText = GetComponentInChildren<TextMeshProUGUI>();
        EM = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().equipmentManager;
        IM = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().inventoryManager;
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().player;
        SetColor(0);

        if(CountText == null)
        {
            CountText= gameObject.AddComponent<TextMeshProUGUI>();
        }

        if(item == null && skill == null)
        {
            itemImage[1].enabled = false;
            CountText.text = "";
        }
        else if(skill != null)
        {
            itemImage[1].sprite = skill.icon;
            SetColor(1);
            itemImage[1].enabled = true;
            CountText.text = "";
            CountText.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetKeyCode(KeyCode keys)
    {
        string temp = keys.ToString();
        if(KeyCode != null)
        {
            if(temp.Contains("Alpha"))
            {
                string replace = temp.Replace("Alpha", "");
                KeyCode.text = replace;
            }
            else
            {
                KeyCode.text = temp;
            }
        }
    }


    Coroutine coroutine;

    public void SkillUse()
    {
        if(SkillUsed == false)
        {
            coroutine = StartCoroutine(Cooltime());
        }
    }

    private IEnumerator Cooltime()
    {
        float cool = skill.cooltime;
        itemImage[1].sprite = skill.Cooltimeicon;
        itemImage[1].enabled = true;
        SetColor(1);
        while (cool > 0.0f)
        {
            SkillUsed = true;
            cool -= Time.deltaTime;
            
            itemImage[1].fillAmount = cool / skill.cooltime;

            if (cool <= 0.0f)
            {
                itemImage[1].fillAmount = 1f;
                itemImage[1].sprite = skill.icon;
                itemImage[1].enabled = true;
                SetColor(1);
                cool = skill.cooltime;
                SkillUsed = false;
                StopCoroutine(coroutine);
            }

            yield return new WaitForFixedUpdate();
        }
    }

    public void AddSkill(Skill Inskill)
    {
        skill = Inskill;
        itemImage[1].sprite = Inskill.icon;
        itemImage[1].enabled = true;

        CountText.text = "0";
        CountText.enabled = false;

        SetColor(1);
    }

    public void Additem(Item Initem, int InCount)
    {
        item = Initem;
        itemCount = InCount;   
        itemImage[1].sprite = Initem.icon;
        itemImage[1].enabled = true;
        
        if (item.itemType != Item.ItemType.Equipment)
        {
            CountText.enabled = true;
            CountText.text = itemCount.ToString();
        }
        else
        {
            CountText.text = "0";
            CountText.enabled = false;
        }

        SetColor(1);
    }

    private void ClearSlot()
    {
        item = null;
        skill = null;
        itemCount = 0;
        itemImage[1].sprite = null;
        SetColor(0);

        CountText.text = "0";
        CountText.enabled = false;
    }

    public void AddCount(int Count)
    {
        itemCount += Count;
        CountText.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }

    private void SetColor(float _alpha)
    {
        Color color = itemImage[1].color;
        color.a = _alpha;
        itemImage[1].color = color;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                if(gameObject.CompareTag("InvenSlot"))
                {
                    if (item.itemType == Item.ItemType.Equipment)
                    {
                        EM.InputItem(item);
                        AddCount(-1);
                    }
                    else if (item.itemType == Item.ItemType.Used)
                    {
                        if (item is UsedItem v)
                        {
                            ps.Healing(v.HealHP, v.HealMP);
                            AddCount(-1);
                        }
                    }
                }
                else if(gameObject.CompareTag("EquipSlot"))
                {
                    if (item.itemType == Item.ItemType.Equipment)
                    {
                        IM.InputItem(item);
                        AddCount(-1);
                    }
                }
                else if(gameObject.CompareTag("QuickSlot"))
                {
                    if (item.itemType == Item.ItemType.Used)
                    {
                        if (item is UsedItem v)
                        {
                            ps.Healing(v.HealHP, v.HealMP);
                            AddCount(-1);
                        }
                    }
                }

            }
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (item != null || skill != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (item != null || skill != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage[1]);
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            if(gameObject.CompareTag("InvenSlot") || gameObject.CompareTag("QuickSlot"))
            {
                ChangeSlot();
            }
        }
    }

    private void ChangeSlot()
    {
        Item tempitem = item;
        int tempitemCount = itemCount;
        Skill tempskill = skill;

        if(DragSlot.instance.dragSlot.item != null)
        {
            Additem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);
        }
        else if(DragSlot.instance.dragSlot.skill != null)
        {
            AddSkill(DragSlot.instance.dragSlot.skill);
        }  

        if (tempitem != null)
        {
            DragSlot.instance.dragSlot.Additem(tempitem, tempitemCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }

        if (tempskill != null)
        {
            DragSlot.instance.dragSlot.AddSkill(tempskill);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }
}
