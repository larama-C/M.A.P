using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class CurveMovealbe : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] Monster;
    public GameObject BossMonster;

    public GameObject TargetMonster;

    public float Dis;

    public AnimationCurve curve;
    public Animator anim;
    public float speed = 1.0f;
    private float hoverHeight = 1f;
    public Vector2 boxSize;

    public int damage = 10;
    int attackCount = 0;
    private bool OnTarget = false;

    BlackJack black;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        black = gameObject.GetComponentInParent<BlackJack>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Monster = GameObject.FindGameObjectsWithTag("Monster");
        Dis = Vector2.Distance(Player.transform.position, Monster[0].transform.position);
        ChaseMonster();
    }

    void ChaseMonster()
    {
        Monster = GameObject.FindGameObjectsWithTag("Monster");
        if(Monster != null)
        {
            foreach (GameObject Close in Monster)
            {
                float Distance = Vector2.Distance(Player.transform.position, Close.transform.position);
                if (Distance <= Dis)
                {
                    TargetMonster = Close;
                    Dis = Distance;
                }
            }
        }
        else
        {
            TargetMonster = null;
            Dead();
        }
    }

    void Dead()
    {
        if (gameObject != null)
        {
            anim.SetBool("ISFINALE", true);
            Destroy(gameObject, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void Generate(Vector3 set)
    {
        StartCoroutine(IEFlight(set));
    }

    private IEnumerator IEFlight(Vector3 SET)
    {
        gameObject.transform.position = SET;
        gameObject.SetActive(true);
        float duration = speed;
        float time = 0.0f;
        Vector3 start = gameObject.transform.position;
        Vector3 end = new Vector3();

        ChaseMonster();

        if (TargetMonster != null)
        {
            OnTarget = true;
            end = TargetMonster.transform.position;
        }
        else
        {
            ChaseMonster();
            OnTarget= false;
        }

        if (Monster == null)
        {
            yield return new WaitForSeconds(3f);
            Vector2 temp = gameObject.transform.position;
            end = temp;
            Dead();
        }
        duration = speed;

        while (time < duration && OnTarget == true)
        {
            time += Time.deltaTime;
            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0.0f, hoverHeight, heightT);

            gameObject.transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0.0f, height);

            yield return null;
        }

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(gameObject.transform.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "BossMonster")
            {
                if(OnTarget)
                {
                    StartCoroutine(Delay(collider.GetComponent<MonsterManager>()));
                }
            }
            else
            {
                if (collider.tag == "Monster")
                {
                    if (OnTarget)
                    {
                        StartCoroutine(Delay(collider.GetComponent<MonsterManager>()));
                    }
                }
            }
        }
        yield return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(gameObject.transform.position, boxSize);
    }

    IEnumerator Delay(MonsterManager mm)
    {
        for(float f = speed * (7f - attackCount); f>= 0; f -= speed)
        {
            if(attackCount < 8)
            {
                if(TargetMonster != null)
                {
                    mm.UnderAttack(damage);
                    attackCount++;
                    yield return null;
                }
                else
                {
                    ChaseMonster();
                    StartCoroutine(IEFlight(gameObject.transform.position));
                    yield return null;
                }
                yield return new WaitForSeconds(speed);
            }
            else if(attackCount >= 8 || Monster == null)
            {
                Dead();
                yield return null;
            }
        }
    }
}
