using Assets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class MonsterManager : MonoBehaviour
{
    public string MobName;
    public int Level = 1;
    public int MaxHP = 1000000;
    public int CurHP;
    public int GiveEXP = 10;
    public GameObject Player;
    public PlayerController PlayerCon;
    public PlayerClass PlayerClass;
    public Animator anim;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI LevelText;
    public Slider HPBar;
    public TextMeshProUGUI HPText;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        GiveEXP = Level * 10;
        anim = GetComponent<Animator>();
        CurHP = MaxHP;
        HPBar.maxValue = MaxHP;
        Texting();
    }

    // Update is called once per frame
    void Update()
    {
        HPBar.value = CurHP;
        HPText.text = MaxHP.ToString() + "/" + CurHP.ToString();
        if (CurHP <= 0)
        {
            CurHP= 0;
            Dead();
        }
    }

    public void UnderAttack(int Damage)
    {
        CurHP -= Damage;
        HPBar.value -= Damage;
    }

    void Dead()
    {
        
        PlayerClass.Exp += GiveEXP;
        anim.SetBool("IsDead", true);
        Destroy(gameObject, 0f);
    }

    void Texting()
    {
        NameText.text = MobName.ToString();
        LevelText.text = "LV." + Level.ToString();
    }
}
