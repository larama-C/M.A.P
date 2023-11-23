using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CurveMovealbe : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] Monster;
    public GameObject BossMonster;

    public GameObject TargetMonster;

    public float Dis;

    private MonsterManager mm;

    public AnimationCurve curve;
    public Animator anim;
    public float speed = 1.0f;
    private float hoverHeight = 1f;
    public Vector2 boxSize;

    public GameObject hiteffect;

    public int damage = 10;
    int attackCount = 0;
    private bool OnTarget = false;
    private bool DeadFlag = false;
    BlackJack black;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        black = gameObject.GetComponentInParent<BlackJack>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void ChaseMonster()
    {
        Monster = GameObject.FindGameObjectsWithTag("Monster");
        BossMonster = GameObject.FindGameObjectWithTag("BossMonster");
        if (BossMonster != null)
        {
            TargetMonster = BossMonster;
            mm = TargetMonster.GetComponentInParent<MonsterManager>();
        }
        else if(BossMonster == null)
        {
            
            foreach (GameObject any in Monster)
            {
                int rand;
                rand = Random.Range(0, Monster.Length);
                TargetMonster = Monster[rand];
                mm = TargetMonster.GetComponentInParent<MonsterManager>();
            }

            //if(Monster.Length > 0)
            //{
            //    Dis = Vector2.Distance(Player.transform.position, Monster[0].transform.position);
            //    foreach (GameObject Close in Monster)
            //    {
            //        float Distance = Vector2.Distance(Player.transform.position, Close.transform.position);
            //        if (Distance <= Dis)
            //        {
            //            TargetMonster = Close;
            //            Dis = Distance;
            //        }
            //    }
            //}

            if(Monster.Length <= 0)
            {
                OnTarget = false;
                Invoke("Dead", 3f);
            }
        }
    }

    void Dead()
    {
        if (gameObject != null)
        {
            mm.UnderAttack(damage, hiteffect);
            anim.SetBool("ISFINALE", true);
            Destroy(gameObject, 1f);
            StopAllCoroutines();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(DeadFlag)
        {
            Dead();
        }
    }

    public void Generate()
    {
        StartCoroutine(IEFlight());
    }

    private IEnumerator IEFlight()
    {

        float duration = speed;
        float time = 0.0f;
        Vector3 start = gameObject.transform.position;
        Vector3 end = new Vector3();

        ChaseMonster();

        if (TargetMonster != null)
        {
            OnTarget = true;
            end = TargetMonster.transform.position;
            yield return null;
        }

        if (Monster.Length <= 0)
        {
            yield return new WaitForSeconds(3f);
            OnTarget = false;
            Vector2 temp = gameObject.transform.position;
            end = temp;
            DeadFlag = true;
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
                    yield return null;
                }
            }
            else
            {
                if (collider.tag == "Monster")
                {
                    if (OnTarget)
                    {
                        StartCoroutine(Delay(collider.GetComponent<MonsterManager>()));
                        yield return null;
                    }
                }
            }
        }

        if(collider2Ds.Length <= 0)
        {
            DeadFlag = true;
            yield return null;
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
                if(Monster.Length > 0)
                {
                    if (TargetMonster != null)
                    {
                        mm.UnderAttack(damage, hiteffect);
                        attackCount++;
                        yield return new WaitForSeconds(0.5f);
                    }
                    ChaseMonster();
                    StartCoroutine(IEFlight());
                }
                else
                {
                    DeadFlag = true;
                    yield return null;
                }
            }
            else
            {
                DeadFlag = true;
                yield return null;
            }
        }
    }
}
