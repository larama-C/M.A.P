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
        Equipment,      //장비 아이템
        Used,           //사용 아이템
        Ingredient,     //재료 아이템
        ETC,            //기타 아이템
    }
    public ItemType itemType;

}
