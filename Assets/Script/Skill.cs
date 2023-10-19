
using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    public CurveMovealbe curve;

    public int SkillId;
    public string SkillName;
    public string SkillDescription;
    public int SkillLevel;
    public float damage;
    public float basicdamage;
    public float damageperLevel;
    private int AttackCount;
    private int MonsterCount;
    public float cooltime;

    public enum SkillType
    {
        Passive,        //�нú� ��ų
        Active,         //��Ƽ�� ��ų
        Buff            //���� ��ų
    }

    public string animationName;
    public Sprite icon;
    public Sprite Cooltimeicon;
    private Animator anim;

    void CalDamage()
    {
        damage = basicdamage + (SkillLevel * damageperLevel);   
    }
}