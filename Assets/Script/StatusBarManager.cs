using Assets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarManager : MonoBehaviour
{
    public PlayerClass Ps;
    public GameObject StatusBar;
    public GameObject ExpBar;
    private TextMeshProUGUI[] PlayerStatusText;
    public Image HPBAR;
    public Image MPBAR;
    public Image EXPBAR;

    // Start is called before the first frame update
    void Start()
    {
        HPBAR.fillAmount = 1f;
        MPBAR.fillAmount = 1f;
        EXPBAR.fillAmount = 1f;
        PlayerStatusText = StatusBar.GetComponentsInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        StatusBarUI();
    }

    void StatusBarUI()
    {
        PlayerStatusText[0].text = Ps.HP.ToString() + "/" + Ps.MaxHP.ToString();
        PlayerStatusText[1].text = Ps.MP.ToString() + "/" + Ps.MaxMP.ToString();
        PlayerStatusText[2].text = Ps.PlayerName;
        PlayerStatusText[3].text = "LV." + Ps.Level.ToString();
        HPBAR.fillAmount = (float)Ps.HP / Ps.MaxHP;
        MPBAR.fillAmount = (float)Ps.MP / Ps.MaxMP;
        EXPBAR.fillAmount = (float)Ps.Exp / Ps.NeedExp;
    }
}
