using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour
{
    public Item item;

    private Image DropImage;

    private void Start()
    {
        DropImage = GetComponentInChildren<Image>();
        SetItem();
    }

    void SetItem()
    {
        DropImage.sprite = item.icon;
    }
}