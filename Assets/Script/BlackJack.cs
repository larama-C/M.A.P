using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public enum SkillState
{
    None = 0,
    Press,
    Create,
    Chase,
    Attack,
    Finale
}

public class BlackJack : MonoBehaviour
{
    private static int MaxSize = 3;
    [SerializeField] GameObject BJ;
    private GameObject Player;
    float[] Xs = { -1.5f, 0f, 1.5f};
    float[] Xy = { 0f, 1.5f, 0f};
    private bool Starting = false;

    public float m_speed = 1; // 투사체 속도
    [Space(10f)]
    public float m_distanceFromStart = 3.0f; // 시작 지점을 기준으로 얼마나 꺾일지.
    public float m_distanceFromEnd = 10.0f; // 도착 지점을 기준으로 얼마나 꺾일지.

    public GameObject[] Monster;
    public GameObject BossMonster;

    public GameObject TargetMonster;

    public MonsterManager mm;

    public SkillState BJState = SkillState.None;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        BJ.SetActive(false);
    }

    public void BlackJackPressed()
    {
        Player.GetComponent<PlayerController>().Effect.GetComponent<Animator>().SetTrigger("BlackJackPress");
        BJState = SkillState.Press;
        Starting = true;
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
        else if (BossMonster == null)
        {

            foreach (GameObject any in Monster)
            {
                int rand;
                rand = Random.Range(0, Monster.Length);
                TargetMonster = Monster[rand];
                mm = TargetMonster.GetComponentInParent<MonsterManager>();
            }

            if (Monster.Length <= 0)
            {
                Invoke("Dead", 3f);
            }
        }
        BJState = SkillState.Chase;
    }

    void BlackJackStart()
    {
        if (Player.GetComponent<PlayerController>().Effect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("BlackJackPress") &&
            Player.GetComponent<PlayerController>().Effect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            GameObject[] go = new GameObject[MaxSize];
            for (int i = 0; i < MaxSize; i++)
            {
                go[i] = Instantiate(BJ);
                go[i].name = BJ.name + i;
                go[i].transform.position = new Vector3(Player.transform.position.x + Xs[i], Player.transform.position.y + Xy[i], Player.transform.position.z);
                go[i].SetActive(true);
                ChaseMonster();
                go[i].GetComponent<BezierCurves>().Init(go[i].transform, TargetMonster.transform, m_speed, m_distanceFromStart, m_distanceFromEnd);
            }
            //BJState = SkillState.Create;
            Starting = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Starting)
        {
            BlackJackStart();
        }
        //else if (!Starting && BJState == SkillState.Create || BJState == SkillState.Chase)
        //{
        //    for (int i = 0; i < MaxSize; i++)
        //    {
        //        if (TargetMonster != null)
        //        {
        //            ChaseMonster();
        //            go[i].GetComponent<BezierCurves>().Init(go[i].transform, TargetMonster.transform, m_speed, m_distanceFromStart, m_distanceFromEnd);
        //        }
        //    }
        //}


    }
}
