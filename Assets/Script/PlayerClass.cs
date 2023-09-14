using System.Collections;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu]
    public class PlayerClass : ScriptableObject
    {
        public string PlayerName;
        public int Level = 0;
        public int Exp = 0;
        public int NeedExp = 10;
        public int MAXAP;
        public int AP;
        public int HP;
        public int MaxHP;
        public int MP;
        public int MaxMP;
        public string MainStat = "LUK";
        public string SubStat1 = "DEX";
        public string SubStat2 = "";
        public int MStat;
        public int SubStat;
        public int MNotPerStat;
        public int STR = 4;
        public int STRLEVEL;
        public float STRPer = 0f;
        public int DEX = 4;
        public int DEXLEVEL;
        public float DEXPer = 0f;
        public int INT = 4;
        public int INTLEVEL;
        public float INTPer = 0f;
        public int LUK = 4;
        public int LUKLEVEL;
        public float LUKPer = 0f;
        public float AllPer = 0f;
        public float CriticalDamage;
        public float Critical;
        public int NotPerStat;
        public int Offensive;
        public float OffensivePer;
        public int Mana;
        public float ManaPer;
        public float WeaponExpert;
        public float DamagePer;
        public float FinalDamagePer;
        public float Expert;
        public int Carrers;
        public int Stat;
        public int MinStat = 4;
        public int PlusStat;
        public float MaxDamage;
        public float MinDamage;

        void LeveltoMAXAP()
        {
            MAXAP = Level * 5;
            if (Carrers < 3 && Carrers > 0)
            {
                MAXAP += Carrers * 4;
            }
            else if (Carrers <= 4)
            {
                MAXAP += ((Carrers - 2) * 5) + (4 * 2);
            }
            else
            {
                MAXAP += 0;
            }
        }

        void AllStat()
        {

            STR = (int)((MinStat + STR) * (1 + STRPer) + NotPerStat);
            DEX = (int)((MinStat + DEX) * (1 + DEXPer) + NotPerStat);
            INT = (int)((MinStat + INT) * (1 + INTPer) + NotPerStat);
            LUK = (int)((MinStat + LUK) * (1 + LUKPer) + NotPerStat);
        }

        void APTOSTAT(string STAT, int INITAP)
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
            }
            if(AP > INITAP)
            {
                AP = MAXAP - INITAP;
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