using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    public int playerSpeed = 1;
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

    void PlayerMove()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("IsWalk", true);
            rend.flipX = true;
            rid2D.AddForce(new Vector2(playerSpeed, 0), ForceMode2D.Force);
            //transform.Translate(new Vector3(playerSpeed * Time.deltaTime, 0, 0));
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("IsWalk", true);
            rend.flipX = false;
            rid2D.AddForce(new Vector2(-playerSpeed, 0), ForceMode2D.Force);
            //transform.Translate(new Vector3(-playerSpeed * Time.deltaTime, 0, 0));
        }
        else if(Input.GetKey(KeyCode.LeftAlt))
        {
            rid2D.AddForce(new Vector2(0, 2), ForceMode2D.Force);
            anim.SetBool("IsJump", true);
        }
        else
        {
            anim.SetBool("IsJump", false);
            anim.SetBool("IsWalk", false);
        }
    }
}
