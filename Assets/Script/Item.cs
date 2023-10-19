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
    public int ItemCount = 1;
    public enum ItemType  // ������ ����
    {
        Equipment,      //��� ������
        Used,           //��� ������
        Ingredient,     //��� ������
        ETC,            //��Ÿ ������
    }
    public ItemType itemType;

}
