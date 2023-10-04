using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EquipmentItem : Item
{
    public enum ClassType  // ���� ����
    {
        Worrior,      //����
        Mage,         //������
        Archor,       //�ü�
        Assassin,     //����
        Pirates       //����
    }

    public ClassType classtype;

    public enum EquipmentType  // ��� ����
    {
        WEAPON,
        SUBWEAPON,
        EMBLEM,
        CAP,
        CLOTHES,
        PANTS,
        SHOES,
        GLOVE,
        CAPE,
        SHOULDER,
        RING,
        PENDANT,
        BELT,
        EYEACC,
        EARACC,
        FOREHEAD,
        MEDAL,
        BADGE,
        HEART,
        ANDROID,
        POCKET
    }

    public EquipmentType equipmenttype;

    public int Offensive = 0;
    public float IgnoreDefensive = 0.0f;
    public int Defensive = 0;
    public int Mana = 0;
    public int STR = 0;
    public float STRPer = 0f;
    public int DEX = 0;
    public float DEXPer = 0f;
    public int INT = 0;
    public float INTPer = 0f;
    public int LUK = 0;
    public float LUKPer = 0f;
    public float AllStatPer = 0f;
    public int MaxHP = 0;
    public int MaxMP = 0;

    public float Critical = 0f;
    public float CriticalDamage = 0f;
    public bool IsWeapon;
    public bool UniqueSkill;
}
