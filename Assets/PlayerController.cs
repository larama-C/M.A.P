using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    public int playerSpeed = 1;
    public float JumpForce = 10;
    Rigidbody2D rid2D;
    SpriteRenderer rend;

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
        PlayerMove();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            return;
        }
    }

    void PlayerMove()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("IsWalk", true);
            rend.flipX = true;
            rid2D.AddForce(new Vector2(playerSpeed, 0), ForceMode2D.Force);
            //transform.Translate(new Vector3(playerSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("IsWalk", true);
            rend.flipX = false;
            rid2D.AddForce(new Vector2(-playerSpeed, 0), ForceMode2D.Force);
            //transform.Translate(new Vector3(-playerSpeed * Time.deltaTime, 0, 0));
        }
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            rid2D.velocity = new Vector2(0, JumpForce);
            anim.SetBool("IsJump", true);
        }
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
