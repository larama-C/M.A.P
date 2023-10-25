using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BlackJack : MonoBehaviour
{
    private static int MaxSize = 3;
    [SerializeField] GameObject BJ;
    [SerializeField] GameObject[] go = new GameObject[MaxSize];
    public GameObject Player;
    float[] Xs = { -1.5f, 0f, 1.5f};
    float[] Xy = { 0f, 1.5f, 0f};

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void BlackJackPressed()
    {
        for (int i = 0; i < MaxSize; i++)
        {
            go[i] = Instantiate(BJ);
            go[i].name = BJ.name + i;
            go[i].transform.position = new Vector3(Player.transform.position.x + Xs[i], Player.transform.position.y + Xy[i], Player.transform.position.z);
            go[i].GetComponent<CurveMovealbe>().Generate(go[i].transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
