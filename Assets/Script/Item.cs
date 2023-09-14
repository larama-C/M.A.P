using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public int ItemId;
    public Sprite icon;
    public string ItemName;
    public string ItemType;
}
