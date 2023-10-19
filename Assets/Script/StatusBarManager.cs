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
    [SerializeField] private TextMeshProUGUI EXPText;

    // Start is called before the first frame update
    void Start()
    {
        Ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().player;
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
        PlayerStatusText[0].text = Ps.CurHP.ToString() + "/" + Ps.MaxHP.ToString();
        PlayerStatusText[1].text = Ps.CurMP.ToString() + "/" + Ps.MaxMP.ToString();
        PlayerStatusText[2].text = Ps.PlayerName;
        PlayerStatusText[3].text = "LV." + Ps.Level.ToString();
        EXPText.text = Ps.Exp.ToString() + "/" + Ps.NeedExp.ToString() + "(" + (((float)Ps.Exp / (float)Ps.NeedExp) * 100.0F) + "%)";
        HPBAR.fillAmount = (float)Ps.CurHP / Ps.MaxHP;
        MPBAR.fillAmount = (float)Ps.CurMP / Ps.MaxMP;
        EXPBAR.fillAmount = (float)Ps.Exp / Ps.NeedExp;
        //StartCoroutine(ImageDelayCoroutine(EXPBAR));
    }

    IEnumerator ImageDelayCoroutine(Image img) 
    {
        while (img.fillAmount < 1)
        {
            img.fillAmount += (float)(Ps.Exp / Ps.NeedExp) * Time.deltaTime;

            yield return null;
        }
    }
}
