using System.Collections;
using UnityEngine;



namespace Assets
{
    public class Player
    {
        public string PlayerName;
        public int Level;
        public int MAXAP;
        public int AP;
        public int Exp;
        public int HP;
        public int MaxHP;
        public int MP;
        public int MaxMP;
        public int STR;
        public int STRLEVEL;
        public int STRPer;
        public int DEX;
        public int DEXLEVEL;
        public int DEXPer;
        public int INT;
        public int INTLEVEL;
        public int INTPer;
        public int LUK;
        public int LUKLEVEL;
        public int LUKPer;
        public int AllPer;
        public int NotPerStat;
        public int Offensive;
        public int Mana;
        public int Carrers;
        public int Stat;
        public int MinStat = 4;
        public int PlusStat;
        public int MaxDamage;
        public int MinDamage;

        void LeveltoStat()
        {
            if(Carrers < 3 && Carrers > 0)
            {
                PlusStat = Carrers * 4;
            }
            else if( Carrers <= 4)
            {
                PlusStat = ((Carrers - 2) * 5) + (4 * 2); 
            }
            else
            {
                PlusStat = 0;
            }
            Stat = MinStat + Level * 5 + PlusStat;
        }

        void AllStat()
        {
            STR = (MinStat + Stat) * (1 + STRPer) + NotPerStat;
            DEX = (MinStat + Stat) * (1 + DEXPer) + NotPerStat;
            INT = (MinStat + Stat) * (1 + INTPer) + NotPerStat;
            LUK = (MinStat + Stat) * (1 + LUKPer) + NotPerStat;
        }


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