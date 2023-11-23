
using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    public PhantomSkill SkillId;
    public string SkillName;
    public string SkillDescription;
    public int SkillLevel;
    public float damage;
    public float basicdamage;
    public float damageperLevel;
    private int AttackCount;
    private int MonsterCount;
    public float cooltime;

    public PlayerClass ps;

    public enum SkillType
    {
        Passive,        //패시브 스킬
        Active,         //액티브 스킬
        Buff            //버프 스킬
    }

    public string animationName;
    public Sprite icon;
    public Sprite Cooltimeicon;
    private Animator anim;



    int printSkillLevel()
    {
        return SkillLevel;
    }

    void CalDamage()
    {
        ps.CalDamage();
        damage = basicdamage + (SkillLevel * damageperLevel);   
    }
}