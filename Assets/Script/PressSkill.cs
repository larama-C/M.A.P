using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PressSkill : MonoBehaviour
{
    public GameObject Player;
    private PlayerController pcon;
    public GameObject Skill;
    public GameObject Skill_Back;           //*Eltimate Drive
    public Animator Frontanim;
    public Animator Backanim;
    public float speed = 1.0f;
    public int damage = 10;
    [SerializeField] private bool Attack = false;

    public Collider2D HitBox;
    public Vector2 boxSize;

    public GameObject hiteffect;

    float delaytime = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        Frontanim = Skill.GetComponent<Animator>();
        Backanim = Skill_Back.GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        pcon = Player.GetComponent<PlayerController>();
        HitBox = Skill.GetComponent<Collider2D>();
    }

    public void CheckPress(bool yes)
    {
        Player.GetComponent<PlayerController>().anim.SetBool("Press", yes);
        if (yes)
        {
            pcon.Effect.GetComponent<Animator>().SetTrigger("Press");
        }
        Attack = yes;
    }

    public void Dead()
    {
        Attack = false;
        Frontanim.SetBool("PressF", false);
        Backanim.SetBool("PressB", false);
        Skill.SetActive(false);
        Skill_Back.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Attack)
        {
            AttackSkill();
        }
        else
        {
            Dead();
        }
    }

    void AttackSkill()
    {
        Frontanim.SetBool("PressF", true);
        Backanim.SetBool("PressB", true);
        if (pcon.Arrowflag == PlayerController.ArrowState.Right)
        {
            Skill.GetComponent<SpriteRenderer>().flipX = true;
            Skill_Back.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(pcon.Arrowflag == PlayerController.ArrowState.Left)
        {
            Skill.GetComponent<SpriteRenderer>().flipX = false;
            Skill_Back.GetComponent<SpriteRenderer>().flipX = false;
        }
        Skill.transform.position = new Vector3 (HitBox.transform.position.x, HitBox.transform.position.y);
        Skill_Back.transform.position = new Vector3(pcon.Back.transform.position.x, pcon.Back.transform.position.y + 0.2f);
        Skill.SetActive(true);
        Skill_Back.SetActive(true);
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(HitBox.transform.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "BossMonster")
            {
                StartCoroutine(Delay(collider.GetComponent<MonsterManager>()));
            }
            else if (collider.tag == "Monster")
            {
                StartCoroutine(Delay(collider.GetComponent<MonsterManager>()));
            }
        }
    }

    IEnumerator Delay(MonsterManager mm)
    {
        mm.UnderAttack(damage, hiteffect);
        yield return new WaitForSeconds(delaytime);
    }
}
