using System.Collections;
using UnityEngine;



namespace Assets
{
    public class Player
    {
        public string PlayerName;
        public int Level;
        public int Exp;
        public int MAXAP;
        public int AP;
        public int HP;
        public int MaxHP;
        public int MP;
        public int MaxMP;
        public int STR;
        public int STRLEVEL;
        public float STRPer;
        public int DEX;
        public int DEXLEVEL;
        public float DEXPer;
        public int INT;
        public int INTLEVEL;
        public float INTPer;
        public int LUK;
        public int LUKLEVEL;
        public float LUKPer;
        public float AllPer;
        public int NotPerStat;
        public int Offensive;
        public int Mana;
        public int Carrers;
        public int Stat;
        public int MinStat = 4;
        public int PlusStat;
        public int MaxDamage;
        public int MinDamage;

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

        //public int Damage()
        //{
        //    int NowDamage;

            

        //    return NowDamage;
        //}

    }
    public class PlayerClass : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}