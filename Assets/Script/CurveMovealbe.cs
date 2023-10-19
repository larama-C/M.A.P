using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CurveMovealbe : MonoBehaviour
{
    public GameObject Player;
    public GameObject Monster;
    public GameObject BossMonster;

    public AnimationCurve curve;
    public Animator anim;
    public float speed = 1.0f;
    private float hoverHeight = 1f;
    public Vector2 boxSize;

    public int damage = 10;
    int attackCount = 0;
    public bool Startflag = false;
    private bool Deadflag = false;

    GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Monster = GameObject.FindGameObjectWithTag("Monster");
    }

    // Update is called once per frame
    void Update()
    {
        if (Startflag)
        {
            StartCoroutine(IEFlight());
        }
        if(Deadflag)
        {
            if(go != null)
            {
                anim.SetBool("ISFINALE", Deadflag);
                Destroy(go, 1f);
            }
        }
    }

    private IEnumerator IEFlight()
    {
        go = Instantiate(this.gameObject);
        go.transform.position = new Vector2(Player.transform.position.x - 0.6f, Player.transform.position.y);
        go.SetActive(true);
        float duration = speed;
        float time = 0.0f;
        Vector3 start = go.transform.position;
        Vector3 end = Monster.transform.position;
        anim = go.GetComponent<Animator>();

        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0.0f, hoverHeight, heightT);

            go.transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0.0f, height);
            yield return null;
        }

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(go.transform.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Monster")
            {
                StartCoroutine(Delay(collider.GetComponent<MonsterManager>()));
            }
        }
    }

    IEnumerator Delay(MonsterManager mm)
    {
        for(float f = speed * 7f; f>= 0; f -= speed)
        {
            if(attackCount < 8)
            {
                mm.UnderAttack(damage);
                attackCount++;
                Debug.Log(attackCount);
                if(attackCount >= 8)
                {
                    Startflag = false;
                    Deadflag = true;
                }
                yield return new WaitForSeconds(speed);
            }
        }
    }
}
