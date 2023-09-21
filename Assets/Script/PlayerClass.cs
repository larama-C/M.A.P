using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets
{
    [CreateAssetMenu]
    public class PlayerClass : ScriptableObject
    {
        public string PlayerName;
        public int Level = 0;
        public string Job;
        public string Guild;
        public int Popularity;
        public int Exp = 0;
        public int NeedExp = 10;
        public int MAXAP;
        public int AP;
        public int UsedAP;
        public int HP = 100;
        public int MaxHP = 100;
        public int MP = 100;
        public int MaxMP = 100;
        public string MainStat = "LUK";
        public string SubStat1 = "DEX";
        public string SubStat2 = "";
        public int MStat;
        public int SubStat;
        public int MNotPerStat;
        public int STR = 4;
        public int STRLEVEL = 0;
        public float STRPer = 0f;
        public int DEX = 4;
        public int DEXLEVEL = 0;
        public float DEXPer = 0f;
        public int INT = 4;
        public int INTLEVEL = 0;
        public float INTPer = 0f;
        public int LUK = 4;
        public int LUKLEVEL = 0;
        public float LUKPer = 0f;
        public float AllPer = 0f;
        public float CriticalDamage = 0.0f;
        public float Critical = 0.0f;
        public int NotPerStat = 0;
        public int Offensive = 10;
        public float OffensivePer = 0.0f;
        public float IgnoreDefensive = 0.0f;
        public float Defensive = 0.0f;
        public int Tolerance = 0;
        public int Mana = 10;
        public float ManaPer = 0.0f;
        public int Speed = 10;
        public float Jump = 10.0f;
        public float WeaponExpert = 1.3f;
        public float DamagePer = 0.0f;
        public float BossDamagePer = 0.0f;
        public float BuffIncrease = 0.0f;
        public float ItemIncrease = 0.0f;
        public float MesoIncrease = 0.0f;
        public float AttackSpeed = 0.0f;
        public float Stance = 0.0f;
        public int StarForce = 0;
        public int ArcaneForce = 0;
        public int AuthenticForce = 0;
        public int Honor = 0;
        public float FinalDamagePer = 0.0f;
        public float Expert = 0.0f;
        public int Carrers = 0;
        public int Stat = 0;
        public int MinStat = 4;
        public int PlusStat = 0;
        public float MaxDamage = 0.0f;
        public float MinDamage = 0.0f;
        bool IsDead = false;
      
        IEnumerator WaitForIt()
        {
            yield return new WaitForSeconds(1.0f);
        }


        public void StartSet()
        {
            AP = MAXAP - UsedAP;
            HP = MaxHP;
            MP = MaxMP;
        }

        public void Check()
        {
            if(HP > MaxHP)
            {
                HP = MaxHP;
            }
            if(MP > MaxMP)
            {
                MP = MaxMP;
            }
        }

        public string StatReturn(string Statname)
        {
            return this.GetType().GetField(Statname).GetValue(this).ToString();
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
            MaxDamage = ((MStat * 4 + SubStat) * 0.01f) * (Offensive) * (1.0f + OffensivePer) * WeaponExpert * (1.0f+DamagePer) * (1.0f +FinalDamagePer);
            MinDamage = Expert * MaxDamage;
        }

        public float CalDamage()
        {
            float NowDamage = 0;

            float Rand;
            Rand = Random.Range(MinDamage, MaxDamage);
            NowDamage = Rand;

            Debug.Log(NowDamage);
            return NowDamage;
        }

    }
}