using Assets;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum PhantomSkill
{
    //0��
    PhantomShroud = 10001,
    HighDex,
    StealSkill,
    SkillManageMent,
    JudgeMent,
    //1��
    DoublePiercing = 10011,
    TalentOfPhantom1,
    SwiftPhantom,
    QuickEvasion,
    //2��
    CallOfFATE = 10021,
    TalentOfPhantom2,
    BrizCarte,
    BlangCarte,
    KaneAccelation,
    KaneMastery,
    LukMonopoly,
    //3��
    CortOfArms = 10031,
    PhantomCharge,
    TalentOfPhantom3,
    LukOfPhantom,
    MissFortune,
    MoonLight,
    FlushAndPly,
    AccuteScence,
    //4��
    EltimateDrive = 10041,
    TalentOfPhantom4,
    TempestOfCard,
    Twilight,
    NoirCarte,
    SoulSteal,
    PreyOfAria,
    MapleWarrior,
    WarriorWill,
    KaneExpert,
    //�����۽�ų
    TOCDamage = 10051,
    TOCCooltime,
    TOCExtra,
    EDDamage,
    EDExtra,
    EDIgnore,
    TOPDamage,
    TOPPersist,
    TOPInhence,
    //�����۽�ų ��Ƽ��
    TalentOfPhantomH = 10061,
    RoseCarteFianle,
    HeroesOfOath,
    //5��
    VenomBurst = 10071,
    FreudsWisdom,
    ReadyToDie,
    MapleBlessing,
    Joker,
    BlackJack,
    MarkOfPhantom,
    RiftBreak,
    //6��
    TempestOfCard6 = 10081,
    DefyingFate,
}


public class SkillManager : MonoBehaviour
{
    public GameObject[] skills;
    public GameObject Player;
    public GameObject Monster;
    public GameObject BossMonster;
    public GameObject SkillParent;
    public List<Skill> skillList = new List<Skill>((int)PhantomSkill.DefyingFate + 1);

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Monster = GameObject.FindGameObjectWithTag("Monster");
        BossMonster = GameObject.FindGameObjectWithTag("BossMonster");
        skills = GameObject.FindGameObjectsWithTag("Skill");

        //for(int i = 0; i < skills.Length; i++)
        //{
        //    skills[i].SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
