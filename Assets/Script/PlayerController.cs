using Assets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public enum State  // 현재 상태
    {
        Stand,     
        Walk,         
        Jump,       
        Attack,
        Alert
    }

    public PlayerClass player;
    private Animator anim;
    public int playerSpeed = 1;
    public float JumpForce = 10;
    public int JumpCount = 1;
    public bool IsGround = true;
    Rigidbody2D rid2D;
    SpriteRenderer rend;
    private int StateFlag = 0;
    private bool AttackFlag = false;
    public Transform pos;
    public Vector2 boxSize;
    public GameObject HitBox;

    public List<Skill> skillList = new List<Skill>();

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rid2D = GetComponent<Rigidbody2D>();
        rend = GetComponentInChildren<SpriteRenderer>();
        player.StartSet();
        StateFlag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerAttack();
        player.Check();
        player.LevelUp();
        player.StatusDamage();     
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Monster" || collision.gameObject.tag == "Item")
        {
            if(collision.gameObject.tag == "Monster")
            {
                StateFlag = (int)State.Alert;
            }
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

    void PlayerSkill()
    {
        if(Input.GetKeyDown(KeyCode.Delete))
        {
            
        }
        if (Input.GetKeyUp(KeyCode.Delete))
        {

        }
    }

    void PlayerAttack()
    {
        //플레이어 공격 모션 랜덤 
        int Rand;
        Rand = Random.Range(1, 4); 
        if(Input.GetKey(KeyCode.LeftControl) && AttackFlag==false)
        {
            StateFlag = (int)State.Attack;
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

    IEnumerator WaitForHeal()
    {
        yield return new WaitForSeconds(10.0f);
        player.CurHP += (int)(player.MaxHP / 0.05f);
        player.CurMP += (int)(player.MaxMP / 0.05f);
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(player.AttackSpeed);
        AttackFlag = false;
    }

    void PlayerMove()
    {
        // 좌우 이동
        if(Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("IsWalk", true);
            rend.flipX = true;
            HitBox.transform.position = new Vector2(gameObject.transform.position.x + 0.6f, gameObject.transform.position.y);
            transform.Translate(Vector2.right * Time.deltaTime * playerSpeed);
            //rid2D.AddForce(new Vector2(playerSpeed, 0), ForceMode2D.Force);
            //rid2D.velocity = new Vector2(playerSpeed, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("IsWalk", true);
            rend.flipX = false;
            HitBox.transform.position = new Vector2(gameObject.transform.position.x - 0.6f, gameObject.transform.position.y);
            transform.Translate(Vector2.left * Time.deltaTime * playerSpeed);
            //rid2D.AddForce(new Vector2(-playerSpeed, 0), ForceMode2D.Force);
            //rid2D.velocity = new Vector2(-playerSpeed, 0);
        }

        // 점프
        if(Input.GetKeyDown(KeyCode.LeftAlt) && JumpCount > 0 && IsGround == true)
        {
            StateFlag = (int)State.Jump;
            JumpCount--;
            IsGround = false;
            rid2D.velocity = new Vector2(0, JumpForce);
            anim.SetBool("IsJump", true);    
        }

        //애니메이션 비활성화
        if(Input.anyKey)
        { 

        }
        else
        {
            StateFlag = (int)State.Stand;
            anim.SetBool("IsJump", false);
            anim.SetBool("IsWalk", false);
        }
    }
}
