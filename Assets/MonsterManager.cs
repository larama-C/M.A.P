using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    public string MobName;
    public int Level;
    public int MaxHP = 1000;
    public int CurHP;
    private Animator anim;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI LevelText;
    public Slider HPBar;
    public TextMeshProUGUI HPText;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        CurHP= MaxHP;
        HPBar.value = MaxHP;
        Texting();
    }

    // Update is called once per frame
    void Update()
    {
        HPText.text = MaxHP.ToString() + "/" + CurHP.ToString();
        if (CurHP <= 0)
        {
            Dead();
        }
    }

    public void UnderAttack(int Damage)
    {
        CurHP -= Damage;
        HPBar.value -= Damage;
        Debug.Log(CurHP);
    }

    void Dead()
    {
        anim.SetBool("IsDead", true);
        Destroy(gameObject, 5f);
    }

    void Texting()
    {
        NameText.text = MobName.ToString();
        LevelText.text = "LV." + Level.ToString();
    }
}
