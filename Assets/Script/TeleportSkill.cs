using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class TeleportSkill : MonoBehaviour
{
    [SerializeField] RaycastHit2D[] hitinfo = new RaycastHit2D[4];      //충돌 구조체 정보

    //플레이어 
    [SerializeField] private GameObject player;

    [SerializeField] PlayerController pcon;

    [SerializeField] Collider2D playercollider;

    [SerializeField] private bool IsTeleport = false;

    //방향 화살표

    Vector2[] arrowVector = { new Vector2(0, -1), new Vector2(0, 1), new Vector2(1, 0), new Vector2(-1, 0) };

    [SerializeField] int[] arrowindex = { 0, 0, 0, 0};

    public GameObject[] ArrowObject = new GameObject[4];

    Color[] raycolor = { Color.red, Color.red, Color.red, Color.red };

    [SerializeField] private bool IsPressed = false;

    [SerializeField] private bool skilluse = false;

    private void Awake()
    {
        if (ArrowObject == null)
        {
            ArrowObject = GameObject.FindGameObjectsWithTag("ShroudShape");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playercollider = player.GetComponent<Collider2D>();
        pcon = player.GetComponent<PlayerController>();
        for (int i = 0; i < ArrowObject.Length; i++)
        {
            ArrowObject[i].SetActive(false);
            arrowindex[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        skilluse = pcon.IsSkillUse;
        IsTeleported();
        TeleportArrow(IsPressed);
        if (pcon.IsSkillUse)
        {
            TeleportInput();
        }
    }

    void TeleportInput()
    {
        Vector2 position = player.transform.position;
        if (pcon.IsGround || pcon.IsScaffold)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && arrowindex[(int)PlayerController.ArrowState.Left] == 0)
            {
                position.x -= (pcon.TeleportDistance + playercollider.bounds.extents.x);
                player.transform.position = position;
                pcon.IsSkillUse = false;
                IsPressed = false;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && arrowindex[(int)PlayerController.ArrowState.Right] == 0)
            {
                position.x += (pcon.TeleportDistance + playercollider.bounds.extents.x);
                player.transform.position = position;
                pcon.IsSkillUse = false;
                IsPressed = false;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && arrowindex[(int)PlayerController.ArrowState.Up] == 0)
            {
                position.y += (pcon.TeleportDistance + playercollider.bounds.extents.y);
                player.transform.position = position;
                pcon.IsSkillUse = false;
                IsPressed = false;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && arrowindex[(int)PlayerController.ArrowState.Down] == 0)
            {
                position.y -= (pcon.TeleportDistance + playercollider.bounds.extents.y);
                player.transform.position = position;
                pcon.IsSkillUse = false;
                IsPressed = false;
            }
            else if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Escape))
            {
                pcon.IsSkillUse = false;
                IsPressed = false;
            }
        }
    }

    void IsTeleported()
    {
        hitinfo[0] = Physics2D.Raycast(playercollider.bounds.center, Vector2.down, pcon.TeleportDistance);
        hitinfo[1] = Physics2D.Raycast(playercollider.bounds.center, Vector2.up, pcon.TeleportDistance);
        hitinfo[2] = Physics2D.Raycast(playercollider.bounds.center, Vector2.right, pcon.TeleportDistance);
        hitinfo[3] = Physics2D.Raycast(playercollider.bounds.center, Vector2.left, pcon.TeleportDistance);

        for (int i = 0; i < hitinfo.Length; i++)
        {
            Vector2 temp = player.transform.position;
            temp.x += arrowVector[i].x;
            temp.y += arrowVector[i].y;
            ArrowObject[i].transform.position = temp;
            if (hitinfo[i].collider != null)
            {
                if (i == 1)
                {
                    if (hitinfo[i].transform.tag == "scaffold")
                    {
                        raycolor[i] = Color.green;
                        arrowindex[i] = 0;
                    }
                    else
                    {
                        raycolor[i] = Color.red;
                        arrowindex[i] = 1;
                    }
                }
                else
                {
                    if (hitinfo[i].transform.tag == "Ground" || hitinfo[i].transform.tag == "Wall")
                    {
                        raycolor[i] = Color.red;
                        arrowindex[i] = 1;
                    }
                    else if (hitinfo[i].transform.tag == "Monster" || hitinfo[i].transform.tag == "BossMonster")
                    {
                        raycolor[i] = Color.green;
                        arrowindex[i] = 0;
                    }
                    else
                    {
                        raycolor[i] = Color.green;
                        arrowindex[i] = 0;
                    }
                }
            }
            else
            {
                if (i != 1)
                {
                    raycolor[i] = Color.green;
                    arrowindex[i] = 0;
                }
            }
            
            
        }

        Debug.DrawRay(playercollider.bounds.center, Vector2.down * pcon.TeleportDistance, raycolor[(int)PlayerController.ArrowState.Down]);
        Debug.DrawRay(playercollider.bounds.center, Vector2.up * pcon.TeleportDistance, raycolor[(int)PlayerController.ArrowState.Up]);
        Debug.DrawRay(playercollider.bounds.center, Vector2.right * pcon.TeleportDistance, raycolor[(int)PlayerController.ArrowState.Right]);
        Debug.DrawRay(playercollider.bounds.center, Vector2.left * pcon.TeleportDistance, raycolor[(int)PlayerController.ArrowState.Left]);
    }

    void TeleportArrow(bool isPreesed)
    {
        int pass = 0;
        for (int i = 0; i < ArrowObject.Length; i++)
        {
            if (ArrowObject[i] != null)
            {
                if (arrowindex[i] == 1)
                {
                    pass++;
                    ArrowObject[i].SetActive(false);
                    if(pass >= 4)
                    {
                        IsPressed = false;
                        pcon.IsSkillUse = false;
                    }
                }
                else
                {
                    ArrowObject[i].SetActive(isPreesed);
                }
            }
        }
    }

    public void Teleport()
    {
        if (IsPressed)
        {
            IsPressed = false;
            pcon.IsSkillUse = false;
            return;
        }
        IsPressed = true;
        pcon.IsSkillUse = true;
    }
}
