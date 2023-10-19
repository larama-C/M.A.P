using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    public PlayerClass Ps;
    public Stats Psstats;
    public GameObject StatusMain;
    public GameObject DetailStatus;

    private TextMeshProUGUI[] PlayerStats;
    public TextMeshProUGUI AP;
    private TextMeshProUGUI[] PlayerDetailStats;
    private Button[] PlayerStatButtons;

    // Start is called before the first frame update
    void Start()
    { 
        PlayerStats = StatusMain.GetComponentsInChildren<TextMeshProUGUI>();
        PlayerStatButtons = StatusMain.GetComponentsInChildren<Button>();
        PlayerDetailStats = DetailStatus.GetComponentsInChildren<TextMeshProUGUI>();
        Ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().player;
    }

    // Update is called once per frame
    void Update()
    {
        Ps.AP = Ps.MAXAP - Ps.UsedAP;
        //PlayerStats[4].text = Ps.StatReturn(PlayerStats[4].name);
        StatusUI();
        if(DetailStatus.activeSelf == true)
        {
            DetailStatusUI();
        }
    }

    void DetailStatusUI()
    {
        PlayerDetailStats[0].text = ((int)Ps.MinDamage).ToString();
        PlayerDetailStats[1].text = ((int)Ps.MaxDamage).ToString();
        PlayerDetailStats[2].text = Ps.DamagePer.ToString() + "%";
        PlayerDetailStats[3].text = Ps.FinalDamagePer.ToString() + "%";
        PlayerDetailStats[4].text = Ps.IgnoreDefensive.ToString() + "%";
        PlayerDetailStats[5].text = Ps.Critical.ToString() + "%";
        PlayerDetailStats[6].text = Ps.CriticalDamage.ToString() + "%";
        PlayerDetailStats[7].text = Ps.Offensive.ToString();
        PlayerDetailStats[8].text = Ps.Tolerance.ToString();
        PlayerDetailStats[9].text = Ps.Defensive.ToString();
        PlayerDetailStats[10].text = Ps.Speed.ToString() + "%";
        PlayerDetailStats[11].text = Ps.Jump.ToString() + "%";
        PlayerDetailStats[12].text = Ps.BossDamagePer.ToString() + "%";
        PlayerDetailStats[13].text = Ps.BuffIncrease.ToString() + "%";
        PlayerDetailStats[14].text = Ps.ItemIncrease.ToString() + "%";
        PlayerDetailStats[15].text = Ps.MesoIncrease.ToString() + "%";
        PlayerDetailStats[16].text = Ps.AttackSpeed.ToString() + "´Ü°è";
        PlayerDetailStats[17].text = Ps.Mana.ToString();
        PlayerDetailStats[18].text = Ps.Stance.ToString() + "%";
        PlayerDetailStats[19].text = Ps.StarForce.ToString();
        PlayerDetailStats[20].text = Ps.ArcaneForce.ToString();
        PlayerDetailStats[21].text = Ps.AuthenticForce.ToString();
        PlayerDetailStats[22].text = Ps.Honor.ToString();
    }

    void StatusUI()
    {
        PlayerStats[0].text = Ps.PlayerName;
        PlayerStats[1].text = Ps.Job;
        PlayerStats[2].text = Ps.Guild;
        PlayerStats[3].text = Ps.Popularity.ToString();
        PlayerStats[4].text = ((int)Ps.MinDamage).ToString();
        PlayerStats[5].text = ((int)Ps.MaxDamage).ToString();
        PlayerStats[6].text = Ps.CurHP.ToString() + "/" + Ps.MaxHP.ToString();
        PlayerStats[7].text = Ps.CurMP.ToString() + "/" + Ps.MaxMP.ToString();
        PlayerStats[8].text = Ps.STR.ToString();
        PlayerStats[9].text = Ps.DEX.ToString();
        PlayerStats[10].text = Ps.INT.ToString();
        PlayerStats[11].text = Ps.LUK.ToString();

        AP.text = Ps.AP.ToString();
    }
    public void PlusStatBtn()
    {
        if (Ps.AP > 0)
        {
            string ButtonName = EventSystem.current.currentSelectedGameObject.name;
            Ps.APTOSTAT(ButtonName, 1);
            Ps.StatusDamage();
        }
        else
        {
            return;
        }
    }
    public void AutoStatBtn()
    {
        if (Ps.AP > 0)
        {
            Ps.APTOSTAT(Ps.MainStat, Ps.AP);
        }
        else
        {
            return;
        }
    }

    public void DetailStatBtn()
    {
        if (DetailStatus.activeSelf == false)
        {
            DetailStatus.SetActive(true);
        }
        else
        {
            DetailStatus.SetActive(false);
        }

    }

    //void statusUI()
    //{
    //    for (int i = 0; i < 12; i++)
    //    {
    //        PlayerStats[i].text = Ps.StatReturn(PlayerStats[i].name);
    //    }

    //}
}
