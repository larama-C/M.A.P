using Assets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerClass player;
    private Animator anim;
    public int playerSpeed = 1;
    public float JumpForce = 10;
    public int JumpCount = 1;
    public bool IsGround = true;
    Rigidbody2D rid2D;
    SpriteRenderer rend;
    private bool AttackFlag = false;
    public Transform pos;
    public Vector2 boxSize;
    public GameObject HitBox;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI LevelText;

    public List<Skill> skillList = new List<Skill>();

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rid2D = GetComponent<Rigidbody2D>();
        rend = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UIUPDATE();
        PlayerMove();
        PlayerAttack();
        player.LevelUp();
        player.StatusDamage();     
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),collision.gameObject.GetComponent<Collider2D>());
            return;
        }
        if(collision.gameObject.tag == "Ground")
        {
            anim.SetBool("IsJump", false);
            IsGround = true;
            JumpCount = 1;
            return;
        }
    }

    void PlayerAttack()
    {
        //�÷��̾� ���� ��� ���� 
        int Rand;
        Rand = Random.Range(1, 4); 
        if(Input.GetKey(KeyCode.LeftControl) && AttackFlag==false)
        {
            AttackFlag = true;
            anim.SetBool("IsAttack", true);
            anim.SetInteger("AttackNum", Rand);
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
            foreach(Collider2D collider in collider2Ds)
            {
                if(collider.tag == "Monster")
                {
                    collider.GetComponent<MonsterManager>().UnderAttack((int)player.CalDamage());
                }
            }
            StartCoroutine(WaitForIt());
        }
        else
        {
            anim.SetBool("IsAttack", false);
            anim.SetInteger("AttackNum", 0);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(1.0f);
        AttackFlag = false;
    }
    void UIUPDATE()
    {
        NameText.text = player.PlayerName;
        LevelText.text = "LV." + player.Level.ToString();
    }

    void PlayerMove()
    {
        // �¿� �̵�
        if(Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("IsWalk", true);
            rend.flipX = true;
            HitBox.transform.position = new Vector2(gameObject.transform.position.x + 0.6f, gameObject.transform.position.y);
            rid2D.AddForce(new Vector2(playerSpeed, 0), ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("IsWalk", true);
            rend.flipX = false;
            HitBox.transform.position = new Vector2(gameObject.transform.position.x - 0.6f, gameObject.transform.position.y);
            rid2D.AddForce(new Vector2(-playerSpeed, 0), ForceMode2D.Force);
        }

        // ����
        if(Input.GetKeyDown(KeyCode.LeftAlt) && JumpCount > 0 && IsGround == true)
        {
            JumpCount--;
            IsGround = false;
            rid2D.velocity = new Vector2(0, JumpForce);
            anim.SetBool("IsJump", true);    
        }

        //�ִϸ��̼� ��Ȱ��ȭ
        if(Input.anyKey)
        { 

        }
        else
        {
            anim.SetBool("IsJump", false);
            anim.SetBool("IsWalk", false);
        }
    }
}