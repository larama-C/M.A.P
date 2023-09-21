using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour
{
    public Item item;
    public int itemCount;
    public Image itemImage;

    private TextMeshProUGUI CountText;

    // Start is called before the first frame update
    void Start()
    {
        CountText = GetComponentInChildren<TextMeshProUGUI>(); 
        itemImage = GetComponentInChildren<Image>();
        SetColor(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (item == null)
        {
            CountText.text = "";
        }
    }

    public void Additem(Item Initem, int InCount)
    {
        Debug.Log(Initem.icon);
        item = Initem;
        itemCount = InCount;
        itemImage.sprite = Initem.icon;

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
        itemCount = 0;
        itemImage.sprite = null;
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
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
}
