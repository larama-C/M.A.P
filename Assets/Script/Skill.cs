
using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    public int SkillId;
    public string SkillName;
    public string SkillDescription;
    public int SkillLevel;
    public float damage;
    private int AttackCount;
    public float cooltime;
    public GameObject HitBox;
    public GameObject Player;
    public GameObject Monster;
    public GameObject BossMonster;

    public string animationName;
    public Sprite icon;
    private Animator anim;
}
