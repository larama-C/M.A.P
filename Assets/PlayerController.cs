using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerSpeed = 10;
    Rigidbody2D rid2D;
    SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rid2D = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
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
            rend.flipX = true;
            rid2D.AddForce(new Vector2(playerSpeed, 0), ForceMode2D.Force);
            //transform.Translate(new Vector3(playerSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rend.flipX = false;
            rid2D.AddForce(new Vector2(playerSpeed, 0), ForceMode2D.Force);
            //transform.Translate(new Vector3(-playerSpeed * Time.deltaTime, 0, 0));
        }
    }
}
