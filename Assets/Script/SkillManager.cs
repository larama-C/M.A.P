using Assets;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SkillManager : MonoBehaviour
{
    public GameObject[] skills;
    public GameObject Player;
    public GameObject Monster;
    public GameObject BossMonster;
    public GameObject SkillParent;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Monster = GameObject.FindGameObjectWithTag("Monster");
        BossMonster = GameObject.FindGameObjectWithTag("BossMonster");
        skills = GameObject.FindGameObjectsWithTag("Skill");

        for(int i = 0; i < skills.Length; i++)
        {
            skills[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            skills[1].GetComponent<CurveMovealbe>().Startflag = true;
            //for (int i = 0; i < skills.Length; i++)
            //{
                
            //}
            //skills = FindFirstObjectByType<GameObject>();
            //Skill[] skillarr = GameObject.FindFirstObjectsByType<Skill>();
            //Skill outskill = null;
            //foreach (var item in skillarr)
            //{
            //    if (item.name == "Black Jack")
            //    {
            //        outskill = item;
            //        break;
            //    }
            //}



            //skills. = GameObject.Find("Black Jack");
        }
    }

    void ChooseMonster()
    {
    }

}
