using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public int ItemId;
    public Sprite icon;
    public string ItemName;
    public enum ItemType  // 아이템 유형
    {
        Equipment,
        Used,
        Ingredient,
        ETC,
    }
    public ItemType itemType;
}
