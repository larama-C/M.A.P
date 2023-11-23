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
        Prone,
        Jump,
        DoubleJump,
        ThreepleJump,
        Attack,
        Alert
    }

    public enum ArrowState
    {
        Down,
        Up,
        Right,
        Left,
        None,
    }

    public PlayerClass player;
    public Animator anim;
    public int playerSpeed = 1;
    public float JumpForce = 10;
    public int JumpCount = 3;
    public bool IsGround = true;
    public bool IsScaffold = false;
    public bool IsSkillUse = false;
    Rigidbody2D rid2D;
    SpriteRenderer rend;
    public State StateFlag = State.Stand;
    private bool AttackFlag = false;
    public Transform pos;
    public Vector2 boxSize;
    public GameObject HitBox;
    public GameObject Back;
    public GameObject Effect;

    public GameObject scaffoldObject;

    public List<Skill> skillList = new List<Skill>();

    [SerializeField] TeleportSkill ts;
    public ArrowState Arrowflag = ArrowState.Left;
    public float TeleportDistance = 3f;

    ColliderController cc;

    public bool FreezeFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<ColliderController>();
        anim = GetComponentInChildren<Animator>();
        rid2D = GetComponent<Rigidbody2D>();
        rend = GetComponentInChildren<SpriteRenderer>();
        player.StartSet();
        StateFlag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        if(!IsSkillUse)
        {
            PlayerMove();
            PlayerJump();
            PlayerAttack();
        }
        //PlayerMove();
        //PlayerJump();
        //PlayerAttack();
        player.Check();
        player.LevelUp();
        player.StatusDamage();     
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "scaffold")
        {
            scaffoldObject = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Monster" || collision.gameObject.tag == "BossMonster" || collision.gameObject.tag == "Item" || collision.gameObject.tag == "Skill")
        {
            if(collision.gameObject.tag == "Monster" || collision.gameObject.tag == "BossMonster")
            {
                StateFlag = State.Alert;
            }
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),collision.gameObject.GetComponent<Collider2D>());
            return;
        }
        if(collision.gameObject.tag == "Ground")
        {
            anim.SetBool("IsJump", false);
            IsGround = true;
            IsScaffold = false;
            JumpCount = 3;
            return;
        }
        if(collision.gameObject.tag == "scaffold")
        {
            scaffoldObject = collision.gameObject;
            anim.SetBool("IsJump", false);
            IsScaffold = true;
            IsGround = false;
            JumpCount = 3;
            return;
        }
    }

    void PlayerAttack()
    {
        //플레이어 공격 모션 랜덤 
        int Rand;
        Rand = Random.Range(1, 4); 
        if(Input.GetKeyDown(KeyCode.LeftControl) && AttackFlag==false)
        {
            StateFlag = State.Attack;
            AttackFlag = true;
            anim.SetBool("IsAttack", true);
            anim.SetInteger("AttackNum", Rand);
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
            foreach(Collider2D collider in collider2Ds)
            {
                if(collider.tag == "Monster")
                {
                    collider.GetComponent<MonsterManager>().UnderAttack((int)player.CalDamage(), null);
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

    //IEnumerator WaitForHeal()
    //{
    //    yield return new WaitForSeconds(10.0f);
    //    player.CurHP += (int)(player.MaxHP / 0.05f);
    //    player.CurMP += (int)(player.MaxMP / 0.05f);
    //}

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(player.AttackSpeed);
        AttackFlag = false;
    }


    [ContextMenu("[callFN]")]
    protected void _Editor_Test()
    {

        GetComponent<Rigidbody2D>().constraints = GetComponent<Rigidbody2D>().constraints & ~RigidbodyConstraints2D.FreezePositionX;
    }

    [ContextMenu("[callFN2]")]
    protected void _Editor_Test2()
    {

        GetComponent<Rigidbody2D>().constraints = GetComponent<Rigidbody2D>().constraints | RigidbodyConstraints2D.FreezePositionX;
    }

    void PlayerJump()
    {
        //윗 점프
        if (Arrowflag == ArrowState.Up && Input.GetKey(KeyCode.LeftAlt) && IsGround == false && IsScaffold == false && StateFlag == State.Jump)
        {
            StateFlag = State.Jump;
            IsScaffold = false;
            IsGround = false;
            float HighForce = JumpForce * 2f;
            rid2D.velocity = new Vector2(0, HighForce);
            anim.SetBool("IsJump", true);
        }

        // 점프
        if (Input.GetKeyDown(KeyCode.LeftAlt) && JumpCount >= 3 && (IsGround == true || IsScaffold == true) && Arrowflag != ArrowState.Down)
        {
            StateFlag = State.Jump;
            JumpCount--;
            IsGround = false;
            IsScaffold = false;
            rid2D.velocity = new Vector2(0, JumpForce);
            anim.SetBool("IsJump", true);
        }
        //더블 점프
        else if(Input.GetKeyDown(KeyCode.LeftAlt) && StateFlag == State.Jump && IsGround == false)
        {
            StartCoroutine(FreezeX(FreezeFlag));
            StateFlag = State.DoubleJump;
            IsGround = false;
            IsScaffold = false;
            JumpCount--;
            rid2D.velocity = new Vector2(JumpForce * cc.xInput, 1f);
            anim.SetBool("IsJump", true);
            Effect.GetComponent<Animator>().SetTrigger("SwitftPhantomPress");
            StartCoroutine(FreezeX(FreezeFlag));
        }
        //트리플 점프
        else if(Input.GetKeyDown(KeyCode.LeftAlt) && StateFlag == State.DoubleJump && IsGround == false)
        {
            StartCoroutine(FreezeX(FreezeFlag));
            StateFlag = State.ThreepleJump;
            IsGround = false;
            IsScaffold = false;
            JumpCount--;
            rid2D.velocity = new Vector2(JumpForce * cc.xInput, 1f);
            //if (Arrowflag == ArrowState.Left)
            //{
            //    rid2D.velocity = new Vector2(-JumpForce, 1f);
            //}
            //else if(Arrowflag == ArrowState.Right)
            //{
            //    rid2D.velocity = new Vector2(JumpForce, 1f);
            //}
            anim.SetBool("IsJump", true);
            Effect.GetComponent<Animator>().SetTrigger("SwitftPhantomPress");
            StartCoroutine(FreezeX(FreezeFlag));
        }
    }

    IEnumerator FreezeX(bool freeze)
    {
        if(!freeze)
        {
            rid2D.constraints = rid2D.constraints & ~RigidbodyConstraints2D.FreezePositionX;
            FreezeFlag = true;
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            yield return new WaitForSeconds(1f);
            rid2D.constraints = rid2D.constraints | RigidbodyConstraints2D.FreezePositionX;
            FreezeFlag = false;
        }
    }

    void Flip()
    {
        if(Arrowflag == ArrowState.Left)
        {
            Effect.GetComponent<SpriteRenderer>().flipX = false;
            HitBox.transform.position = new Vector2(gameObject.transform.position.x - 1.0f, gameObject.transform.position.y);
            Back.transform.position = new Vector2(gameObject.transform.position.x + 1.0f, gameObject.transform.position.y);
        }
        else if(Arrowflag == ArrowState.Right)
        {
            Effect.GetComponent<SpriteRenderer>().flipX = true;
            HitBox.transform.position = new Vector2(gameObject.transform.position.x + 1.0f, gameObject.transform.position.y);
            Back.transform.position = new Vector2(gameObject.transform.position.x - 1.0f, gameObject.transform.position.y);
        }
        
    }
    private IEnumerator Disablecollision()
    {
        Collider2D scaffoldcollider = scaffoldObject.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), scaffoldcollider);
        yield return new WaitForSeconds(0.50f);
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), scaffoldcollider, false);
    }

    void PlayerMove()
    {
        if(cc.isOnSlope && cc.canWalkOnSlope)
        {
            Vector2 newVelocity = new Vector2();
            newVelocity.Set(playerSpeed * cc.slopeNormalPerp.x * -cc.xInput, playerSpeed * cc.slopeNormalPerp.y * -cc.xInput);
            rid2D.velocity = newVelocity;
        }
        // 좌우 이동
        if(Input.GetKey(KeyCode.RightArrow))
        {
            if(IsGround)
            {
                StateFlag = State.Walk;
            }
            anim.SetBool("IsWalk", true);
            rend.flipX = true;
            Arrowflag = ArrowState.Right;
            transform.Translate(Vector2.right * Time.deltaTime * playerSpeed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (IsGround)
            {
                StateFlag = State.Walk;
            }
            anim.SetBool("IsWalk", true);
            rend.flipX = false;
            Arrowflag = ArrowState.Left;
            transform.Translate(Vector2.left * Time.deltaTime * playerSpeed);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Arrowflag = ArrowState.Up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Arrowflag = ArrowState.Down;
            if((IsGround == true || IsScaffold == true))
            {
                StateFlag = State.Prone;
                //transform.Translate(Vector2.down * Time.deltaTime * playerSpeed);
                anim.SetBool("IsWalk", false);
                anim.SetBool("IsProne", true);
                if(StateFlag == State.Prone && IsScaffold == true && IsGround == false && Input.GetKeyDown(KeyCode.LeftAlt) && scaffoldObject != null)
                {
                    StartCoroutine(Disablecollision());
                }
            }
        }
        else if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            Arrowflag = ArrowState.None;
            StateFlag = State.Stand;
            anim.SetBool("IsProne", false);
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && (IsGround || IsScaffold))
        {
            StateFlag = State.Walk;
            ts.Teleport();
        }

        //애니메이션 비활성화
        if (Input.anyKey)
        { 

        }
        else
        {
            Arrowflag = ArrowState.None;
            anim.SetBool("IsJump", false);
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsProne", false);
        }
    }
}
