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
    public TextMeshProUGUI DamageText;

    public float TextSpeed = 2.0f;
    Color alpha;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Level = PlayerClass.Level;
        GiveEXP = Level * 10;
        MaxHP = Level * 10;
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
        DamageText.text = Damage.ToString();
        DamageText.gameObject.transform.Translate(new Vector2(0, TextSpeed * Time.deltaTime)); // 텍스트 위치

        //alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * TextSpeed); // 텍스트 알파값
        //DamageText.color = alpha;
    }

    void Dead()
    {
        PlayerClass.Exp += GiveEXP;
        anim.SetBool("IsDead", true);
        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }

    void Texting()
    {
        NameText.text = MobName.ToString();
        LevelText.text = "LV." + Level.ToString();
    }
}
