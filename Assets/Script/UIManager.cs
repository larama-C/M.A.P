using Assets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerClass Ps;
    public TextMeshProUGUI PlayerNameText;
    public TextMeshProUGUI PlayerLevelText;
    public Image PlayerHPBar;
    public TextMeshProUGUI PlayerHealthText;
    public Image PlayerMPBar;
    public TextMeshProUGUI PlayerManaText;
    public Image PlayerEXPBar;
    public GameObject StatusWindow;

    public List<TextMeshProUGUI> PlayerStatList;
    public List<Button> PlayerStatButtonList;
    // Start is called before the first frame update
    void Start()
    {
        PlayerHPBar.fillAmount= 1f;
        PlayerMPBar.fillAmount= 1f;
        PlayerEXPBar.fillAmount= 1f;
    }
    void UIUPDATE()
    {
        PlayerHPBar.fillAmount = (float)Ps.HP / Ps.MaxHP;
        PlayerMPBar.fillAmount = (float)Ps.MP / Ps.MaxMP;
        PlayerEXPBar.fillAmount = (float)Ps.Exp / Ps.NeedExp;
        PlayerNameText.text = Ps.PlayerName;
        PlayerLevelText.text = "LV." + Ps.Level.ToString();
        PlayerHealthText.text = Ps.HP.ToString() + "/" + Ps.MaxHP.ToString();
        PlayerManaText.text= Ps.MP.ToString() + "/" + Ps.MaxMP.ToString();
        PlayerStatList[0].text = Ps.PlayerName;
        PlayerStatList[1].text = Ps.Job;
        PlayerStatList[2].text = Ps.Guild;
        PlayerStatList[3].text = Ps.Popularity.ToString();
        PlayerStatList[4].text = Ps.MinDamage.ToString();
        PlayerStatList[5].text = Ps.MaxDamage.ToString();
        PlayerStatList[6].text = Ps.HP.ToString() + "/" + Ps.MaxHP.ToString();
        PlayerStatList[7].text = Ps.MP.ToString() + "/" + Ps.MaxMP.ToString();
        Ps.AP = Ps.MAXAP - Ps.UsedAP;
        PlayerStatList[8].text = Ps.AP.ToString();
        PlayerStatList[9].text = Ps.STR.ToString();
        PlayerStatList[10].text = Ps.DEX.ToString();
        PlayerStatList[11].text = Ps.INT.ToString();
        PlayerStatList[12].text = Ps.LUK.ToString();
    }

    void InputSystem()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if(StatusWindow.active == false)
            {
                StatusWindow.active = true;
            }
            else
            {
                StatusWindow.active = false;
            }
        }
    }

    public void PlusStatBtn()
    {
        if(Ps.AP > 0)
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
        if(Ps.AP > 0)
        {
            Ps.APTOSTAT(Ps.MainStat, Ps.AP);
        }
        else
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UIUPDATE();
        InputSystem();
    }
}
