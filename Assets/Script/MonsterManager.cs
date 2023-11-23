using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public enum MonsterSpecify
{
    None,
    StarForce,
    ArcaneForce,
    AuthenticForce
}

public class MonsterManager : MonoBehaviour
{
    public string MobName;
    public int Level = 1;
    public int MaxHP = 1000000;
    public int CurHP;
    public int GiveEXP = 10;
    public float DefensivePer = 100.0f;
    public int NeedStarForce;
    public int NeedArcaneForce;
    public int NeedAuthenticForce;
    public float MoveSpeed = 1.0f;
    public GameObject Player;
    public PlayerController PlayerCon;
    public PlayerClass PlayerClass;
    public Animator anim;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI LevelText;
    public Slider HPBar;
    public TextMeshProUGUI HPText;
    public GameObject DamageText;
    public Transform hudPos;



    public MonsterSpecify MS;

    [SerializeField] private bool StaticMonster = true;
    public GameObject CognitionBox;
    public Vector2 boxsize;

    public GameObject Monstereffect;
    private Animator effectanim;

    [SerializeField] float MiniDistance = 0.5f;

    private RaycastHit2D hitInfo;  // 충돌체 정보 저장

    private bool IsGround = false;

    [SerializeField] private LayerMask layerMask;

    Color raycolor;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerClass = Player.GetComponent<PlayerController>().player;
        Level = PlayerClass.Level;
        GiveEXP = Level * 10;
        MaxHP = Level * 10000;
        anim = GetComponent<Animator>();
        CurHP = MaxHP;
        HPBar.maxValue = MaxHP;
        Texting();
    }

    // Update is called once per frame
    void Update()
    {
        HPBar.value = CurHP;
        HPText.text = CurHP.ToString() + "/" + MaxHP.ToString();
        if (CurHP <= 0)
        {
            CurHP = 0;
            Dead();
        }
        Move();
        if (!StaticMonster)
        {

            if(IsGround)
            {
                ChasePlayer();
            }
        }
    }

    void Move()
    {
        hitInfo = Physics2D.Raycast(gameObject.GetComponent<Collider2D>().bounds.center, Vector2.down, gameObject.GetComponent<Collider2D>().bounds.extents.y + 1f, layerMask);
        if (hitInfo.collider != null)
        {
            if (hitInfo.transform.tag == "Ground")
            {
                raycolor = Color.green;
                IsGround = true;
            }
            else
            {
                raycolor = Color.red;
                IsGround = false;
            }
        }
        Debug.DrawRay(gameObject.GetComponent<Collider2D>().bounds.center, Vector2.down * 2,raycolor);
    }

    void ChasePlayer()
    {
        float Distance = Vector2.Distance(gameObject.transform.position, Player.transform.position);
        if (Player.transform.position.x > gameObject.transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Distance <= boxsize.x && MiniDistance <= Distance)
        {
            anim.SetBool("Chase", true);
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, MoveSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Chase", false);
        }
    }

    public void UnderAttack(int Damage, GameObject effect)
    {
        GameObject go = null;
        if (effect != null)
        {
            go = Instantiate(effect);
            float range_x = gameObject.GetComponent<Collider2D>().bounds.size.x;
            float range_y = gameObject.GetComponent<Collider2D>().bounds.size.y;
            range_x = Random.Range((range_x / 4) * -1, range_x / 4);
            range_y = Random.Range((range_y / 4), range_y / 4);
            Vector2 rand = new Vector2(gameObject.transform.position.x + range_x, gameObject.transform.position.y + range_y);
            go.transform.position = rand;
            go.SetActive(true);
            effectanim = go.GetComponent<Animator>();
        }
        if (go != null)
        {
            StartCoroutine(EffectEnd(go, Damage));
        }
        else
        {
            Destroy(go);
        }
    }

    IEnumerator EffectEnd( GameObject p_go, int damage)
    {
        effectanim.SetTrigger("Hit");
        Vector2 temp = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + Random.Range(0f, 0.5f));
        CurHP -= damage;
        HPBar.value -= damage;
        GameObject hudText = Instantiate(DamageText);
        hudText.transform.position = temp;
        hudText.GetComponent<DamageText>().damage = damage;
        hudText.SetActive(true);
        while (p_go.GetComponent<Animator>() != null)
        {
            if (effectanim.GetCurrentAnimatorStateInfo(0).IsName("HitEffect") &&
            effectanim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                break;
            }
        }
        Destroy(p_go);
        yield return null;
    }

    void Dead()
    {
        //GameManager.Instance.Exp += GiveEXP;
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
