using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    public string MobName;
    public int Level;
    public int HP = 100;
    private Animator anim;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI LevelText;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        Texting();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(HP);
        if (HP <= 0)
        {
            Dead();
        }
    }

    public void UnderAttack(int Damage)
    {
        HP -= Damage;
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
