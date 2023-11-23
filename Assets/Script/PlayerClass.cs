using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;

namespace Assets
{
    public enum ClassType  // 직업 유형
    {
        Worrior,      //전사
        Mage,         //마법사
        Archor,       //궁수
        Assassin,     //도적
        Pirates       //해적
    }

    public class PlayerData
    {
        public string PlayerName;                               //닉네임
        public int Level = 0;                                   //레벨
        public string Job;
        public ClassType classtype;
        public string Guild;                                    //길드명
        public int Popularity;                                  //인기도
        public int Exp = 0;                                     //현재 경험치
        public int NeedExp = 10;                                //경험치 요구량
        public int MAXAP;                                       //레벨에 따른 최대 투자 스텟
        public int AP;                                          //남아있는 투자 스텟
        public int UsedAP;                                      //투자한 스텟 수치
        public int CurHP = 100;                                 //현재 체력
        public int MaxHP = 100;                                 //최대 체력
        public int CurMP = 100;                                 //현재 마나
        public int MaxMP = 100;                                 //최대 마나
        public string MainStat = "LUK";
        public string SubStat1 = "DEX";
        public string SubStat2 = "";
        public int MStat;                                       //주스텟
        public int SubStat;                                     //부스텟
        public int MNotPerStat;                                 //퍼센트 미반영 스텟
        public int STR = 4;                                     //기초힘스텟
        public int STRLEVEL = 0;                                //힘투자레벨
        public float STRPer = 0f;                               //힘퍼센트
        public int DEX = 4;                                     //기초덱스스텟
        public int DEXLEVEL = 0;                                //덱스투자레벨
        public float DEXPer = 0f;                               //덱스퍼센트
        public int INT = 4;                                     //기초인트스텟
        public int INTLEVEL = 0;                                //인트투자레벨
        public float INTPer = 0f;                               //인트퍼센트
        public int LUK = 4;                                     //럭기초스텟
        public int LUKLEVEL = 0;                                //럭투자레벨
        public float LUKPer = 0f;                               //럭퍼센트
        public float AllPer = 0f;                               //올스텟퍼
        public float CriticalDamage = 0.0f;                     //크리티컬 데미지
        public float Critical = 0.0f;                           //크리티컬 확률
        public int NotPerStat = 0;                              //곱연산 미반영 스텟
        public float IgnoreDefensive = 0.0f;                    //방어율 무시
        public float Defensive = 0.0f;                          //방어력
        public int Tolerance = 0;                               //내성수치
        public int Offensive = 10;                              //공격력
        public float OffensivePer = 0.0f;                       //공격력 퍼센트
        public int Mana = 10;                                   //마력
        public float ManaPer = 0.0f;                            //마력퍼센트
        public int Speed = 10;                                  //이동속도
        public float Jump = 10.0f;                              //점프력
        public float WeaponExpert = 1.3f;                       //무기상수
        public float JobExpert = 1.0f;                          //직업상수
        public float DamagePer = 0.0f;                          //데미지 퍼센트
        public float BossDamagePer = 0.0f;                      //보스데미지 퍼센트
        public float BuffIncrease = 0.0f;                       //버프지속시간 증가
        public float ItemIncrease = 0.0f;                       //아이템 획득률 증가
        public float MesoIncrease = 0.0f;                       //메소 획득량 증가
        public float AttackSpeed = 1.0f;                        //공격 속도
        public float Stance = 0.0f;                             //스탠스 확률
        public int StarForce = 0;                               //스타포스 수치
        public int ArcaneForce = 0;                             //아케인포스 수치
        public int AuthenticForce = 0;                          //어센틱포스 수치
        public int Honor = 0;                                   //명성치
        public float FinalDamagePer = 0.0f;                     //최종데미지 퍼센트
        public float Expert = 0.0f;                             //숙련도
        public int Carrers = 0;                                 //현재 전직 차수
        public int MinStat = 4;                                 //기본 스텟 4
        public float MaxDamage = 0.0f;                          //스텟공격력 최대 데미지
        public float MinDamage = 0.0f;                          //스텟공격력 최소 데미지
        public int Meso = 0;                                    //메소
        public int MaplePoint = 0;                              //메이플 포인트
        public bool IsMage = false;                             //법사 직업군 
        public int MainOM;                                      //공격력/마력
        public float MainOMPer;                                 //공격력/마력 퍼센트
        public float MainDamage;                                //스킬에 적용되는 최종 데미지
        public float skillDamage;                               //스킬 데미지 퍼센트
        public bool IsDead = false;
    }

    [CreateAssetMenu]
    public class PlayerClass : ScriptableObject
    {
        public string PlayerName;                               //닉네임
        public int Level = 0;                                   //레벨
        public string Job;
        public ClassType classtype;
        public string Guild;                                    //길드명
        public int Popularity;                                  //인기도
        public int Exp = 0;                                     //현재 경험치
        public int NeedExp = 10;                                //경험치 요구량
        public int MAXAP;                                       //레벨에 따른 최대 투자 스텟
        public int AP;                                          //남아있는 투자 스텟
        public int UsedAP;                                      //투자한 스텟 수치
        public int CurHP = 100;                                 //현재 체력
        public int MaxHP = 100;                                 //최대 체력
        public int CurMP = 100;                                 //현재 마나
        public int MaxMP = 100;                                 //최대 마나
        public string MainStat = "LUK";
        public string SubStat1 = "DEX";
        public string SubStat2 = "";
        public int MStat;                                       //주스텟
        public int SubStat;                                     //부스텟
        public int MNotPerStat;                                 //퍼센트 미반영 스텟
        public int STR = 4;                                     //기초힘스텟
        public int STRLEVEL = 0;                                //힘투자레벨
        public float STRPer = 0f;                               //힘퍼센트
        public int DEX = 4;                                     //기초덱스스텟
        public int DEXLEVEL = 0;                                //덱스투자레벨
        public float DEXPer = 0f;                               //덱스퍼센트
        public int INT = 4;                                     //기초인트스텟
        public int INTLEVEL = 0;                                //인트투자레벨
        public float INTPer = 0f;                               //인트퍼센트
        public int LUK = 4;                                     //럭기초스텟
        public int LUKLEVEL = 0;                                //럭투자레벨
        public float LUKPer = 0f;                               //럭퍼센트
        public float AllPer = 0f;                               //올스텟퍼
        public float CriticalDamage = 0.0f;                     //크리티컬 데미지
        public float Critical = 0.0f;                           //크리티컬 확률
        public int NotPerStat = 0;                              //곱연산 미반영 스텟
        public float IgnoreDefensive = 0.0f;                    //방어율 무시
        public float Defensive = 0.0f;                          //방어력
        public int Tolerance = 0;                               //내성수치
        public int Offensive = 10;                              //공격력
        public float OffensivePer = 0.0f;                       //공격력 퍼센트
        public int Mana = 10;                                   //마력
        public float ManaPer = 0.0f;                            //마력퍼센트
        public int Speed = 10;                                  //이동속도
        public float Jump = 10.0f;                              //점프력
        public float WeaponExpert = 1.3f;                       //무기상수
        public float JobExpert = 1.0f;                          //직업상수
        public float DamagePer = 0.0f;                          //데미지 퍼센트
        public float BossDamagePer = 0.0f;                      //보스데미지 퍼센트
        public float BuffIncrease = 0.0f;                       //버프지속시간 증가
        public float ItemIncrease = 0.0f;                       //아이템 획득률 증가
        public float MesoIncrease = 0.0f;                       //메소 획득량 증가
        public float AttackSpeed = 1.0f;                        //공격 속도
        public float Stance = 0.0f;                             //스탠스 확률
        public int StarForce = 0;                               //스타포스 수치
        public int ArcaneForce = 0;                             //아케인포스 수치
        public int AuthenticForce = 0;                          //어센틱포스 수치
        public int Honor = 0;                                   //명성치
        public float FinalDamagePer = 0.0f;                     //최종데미지 퍼센트
        public float Expert = 0.0f;                             //숙련도
        public int Carrers = 0;                                 //현재 전직 차수
        public int MinStat = 4;                                 //기본 스텟 4
        public float MaxDamage = 0.0f;                          //스텟공격력 최대 데미지
        public float MinDamage = 0.0f;                          //스텟공격력 최소 데미지
        public int Meso = 0;                                    //메소
        public int MaplePoint = 0;                              //메이플 포인트
        public bool IsMage = false;                             //법사 직업군 
        public int MainOM;                                      //공격력/마력
        public float MainOMPer;                                 //공격력/마력 퍼센트
        public float MainDamage;                                //스킬에 적용되는 최종 데미지
        public float skillDamage;                               //스킬 데미지 퍼센트
        public bool IsDead = false;

        //[SerializeField] public PlayerData pd = new PlayerData();

        //[ContextMenu("To Json Data")]
        //void SavePlayerDataToJson()
        //{
        //    string jsonData = JsonUtility.ToJson(pd);
        //    string path = Path.Combine(Application.dataPath, "playerData.json");
        //    File.WriteAllText(path, jsonData);
        //}

        public void StartSet()
        {
            AP = MAXAP - UsedAP;
            CurHP = MaxHP;
            CurMP = MaxMP;
        }

        public void Check()
        {
            if(CurHP > MaxHP)
            {
                CurHP = MaxHP;
            }
            if(CurMP > MaxMP)
            {
                CurMP = MaxMP;
            }

            if(CurHP <= 0)
            {
                IsDead = true;
            }

            if(IsMage)
            {
                MainOM = Mana;
                MainOMPer = ManaPer;
            }
            else
            {
                MainOM = Offensive;
                MainOMPer = OffensivePer;
            }
        }

        public string StatReturn(string Statname)
        {
            var r = this.GetType().GetField(Statname).GetValue(this).ToString();

            return r;
        }

        void LeveltoMAXAP()
        {
            if (MAXAP != (Level - 1) * 5 )
            {
                MAXAP = (Level - 1) * 5;
            }
            if (Carrers < 3 && Carrers > 0)
            {
                MAXAP += Carrers * 4;
            }
            else if (Carrers >= 3)
            {
                MAXAP += ((Carrers - 2) * 5) + (4 * 2);
            }
            else
            {
                return;
            }
        }

        void AllStat()
        {
            STR = (int)((MinStat + STR) * (1 + STRPer + AllPer) + NotPerStat);
            DEX = (int)((MinStat + DEX) * (1 + DEXPer + AllPer) + NotPerStat);
            INT = (int)((MinStat + INT) * (1 + INTPer + AllPer) + NotPerStat);
            LUK = (int)((MinStat + LUK) * (1 + LUKPer + AllPer) + NotPerStat);
        }

        public void APTOSTAT(string STAT, int INITAP)
        {
            switch (STAT)
            {
                case "STR":
                    STR += INITAP;
                    break;
                case "DEX":
                    DEX += INITAP;
                    break;
                case "INT":
                    INT += INITAP;
                    break;
                case "LUK":
                    LUK += INITAP;
                    break;
                default:
                    return;
            }
            if(AP >= INITAP)
            {
                UsedAP = UsedAP + INITAP;
                AP = MAXAP - UsedAP;
            }
            else
            {
                Debug.Log("스텟 초과");
            }
        }

        public void LevelUp()
        {
            NeedExp = Level * Level * 5;
            if(Exp >= NeedExp)
            {
                Exp = Exp - NeedExp;
                Level = Level + 1;
                LeveltoMAXAP();
            }
        }

        public void StatusDamage()
        {
            AllStat();
            switch(MainStat)
            {
                case "STR":
                    MStat = STR;
                    break;
                case "DEX":
                    MStat = DEX;
                    break;
                case "INT":
                    MStat = INT;
                    break;
                case "LUK":
                    MStat = LUK;
                    break;
                default :
                    return;
            }
            switch (SubStat1)
            {
                case "STR":
                    SubStat = STR;
                    break;
                case "DEX":
                    SubStat = DEX;
                    break;
                case "INT":
                    SubStat = INT;  
                    break;
                case "LUK":
                    SubStat = LUK;
                    break;
                default:
                    return;
            }
            //[(주스텟*4+부스텟)/100]*(총 공/마)*[(100+데미지%)/100]*[(100+최종 데미지)/100]*[(100+공/마%)/100]*무기상수*직업상수
            MaxDamage = ((MStat * 4 + SubStat) * 0.01f) * (MainOM) * ((100.0f + DamagePer) / 0.01f) * ((100.0f + FinalDamagePer) / 0.01f) * 
                ((100.0f + MainOMPer) / 0.01f) * WeaponExpert * JobExpert;
            MinDamage = Expert * MaxDamage;
        }

        void Calculating(MonsterManager mm)
        {
            //데미지 = [(주스탯 * 4 + 부스탯) * 총 공격력 * 무기상수 * 직업보정상수 / 100] * (스킬 퍼뎀 / 100)
            //*(크리티컬 발동시) 크리티컬 데미지 보정 * [(100 + 공격력 %) / 100] * [(100 + 데미지 % +보공 %) / 100]
            //* 방어율 무시 보정 *렙차 보정* 속성 보정 * (아케인포스 필요 적의 경우) 아케인포스 보정 *숙련도 보정
            //* [(모든 최종데미지 계산값 % +100) / 100 ]      (1.1)

            float CalMonsterForce = 1f;
            float MonsterNeedForce = 1f;

            switch (mm.MS)
            {
                case MonsterSpecify.None:
                    MonsterNeedForce = 1f;
                    break;
                case MonsterSpecify.StarForce:
                    MonsterNeedForce = StarForce;
                    break;
                case MonsterSpecify.ArcaneForce:
                    MonsterNeedForce = ArcaneForce;
                    break;
                case MonsterSpecify.AuthenticForce:
                    MonsterNeedForce = AuthenticForce;
                    break;
                default:
                    return;
            }

            float CalDefensive = 1f;

            if(mm.DefensivePer > 0.0f)
            {
                if(IgnoreDefensive >= mm.DefensivePer)
                {
                    CalDefensive = 1f;
                }
                else
                {
                    float temp = 0f;
                    temp = mm.DefensivePer % 100.0f;
                    CalDefensive = temp;
                }
            }

            float CalLevelDamage = 1f;

            if(mm.Level >= Level)
            {
                if(mm.Level + 20 >= Level)
                {
                    CalLevelDamage = 0.8f;
                }
                else
                {
                    CalLevelDamage = (1.0f - ((mm.Level - Level) / 100.0f));
                }
            }
            else
            {
                if(mm.Level + 20 <= Level)
                {
                    CalLevelDamage = 1.2f;
                }
                else
                {
                    CalLevelDamage = 1.0f + ((Level -mm.Level) / 100.0f);
                }
            }

            //데미지 = [(주스탯 * 4 + 부스탯) * 총 공격력 * 무기상수 * 직업보정상수 / 100] * (스킬 퍼뎀 / 100)
            //*(크리티컬 발동시) 크리티컬 데미지 보정 * [(100 + 공격력 %) / 100] * [(100 + 데미지 % +보공 %) / 100]
            //* 방어율 무시 보정 *렙차 보정* 속성 보정 * (아케인포스 필요 적의 경우) 아케인포스 보정 *숙련도 보정
            //* [(모든 최종데미지 계산값 % +100) / 100 ]      (1.1)

            MainDamage = ((MStat * 4 + SubStat) * MainOM * WeaponExpert * JobExpert * 0.01f) * (skillDamage * 0.01f)
            /* (크리티컬 발동시) */ * ((100.0f + MainOMPer) * 0.01f) * ((100.0f + DamagePer) * 0.01f) * ((100.0f + DamagePer + BossDamagePer) * 0.01f)
            * CalDefensive * CalLevelDamage * 1f * 1f * Expert * ((FinalDamagePer + 100) / 100.0f);
        }

        public float CalDamage()
        {
            float NowDamage = 0;

            float Rand;
            Rand = UnityEngine.Random.Range(MinDamage, MaxDamage);
            NowDamage = Rand;

            Debug.Log(NowDamage);
            return NowDamage;
        }

        public void Healing(int HP, int MP)
        {
            if(CurHP < MaxHP || CurMP < MaxMP)
            {
                CurHP += HP;
                CurMP += MP;
            }
        }

    }
}