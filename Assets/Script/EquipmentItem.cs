using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EquipmentItem : Item
{
    public int Offensive;
    public int Mana;
    public int STR;
    public float STRPer = 0f;
    public int DEX;
    public float DEXPer = 0f;
    public int INT;
    public float INTPer = 0f;
    public int LUK;
    public float LUKPer = 0f;
    public float AllStatPer = 0f;
    public float Critical = 0f;
    public float CriticalDamage = 0f;
    public bool IsWeapon;
    public bool UniqueSkill;
}
