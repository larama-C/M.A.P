using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurves : MonoBehaviour
{
    Vector2[] m_points = new Vector2[4];                //������ ��ǥ�迭

    private float m_timerMax = 0;                       //�ִ�ð�
    private float m_timerCurrent = 0;                   //����ð�
    private float m_speed;                              //�̵��ӵ�

    public GameObject hiteffect;                        //����Ʈ

    //���� ��ǥ, ��ǥ ��ǥ, �̵��ӵ�, ���������� �Է¹޾� �������ִ� �Լ� 
    public void Init(Transform _startTr, Transform _endTr, float _speed, float _newPointDistanceFromStartTr, float _newPointDistanceFromEndTr)
    {
        m_speed = _speed;

        // ���� ������ �ð��� �������� �ش�
        m_timerMax = Random.Range(0.8f, 1.0f);

        // ���� ����.
        m_points[0] = _startTr.position;

        // ���� ������ �������� ���� ����Ʈ ����
        m_points[1] = _startTr.position +
            (_newPointDistanceFromStartTr * Random.Range(-1.0f, 1.0f) * _startTr.right) + // X (��, �� ��ü)
            (_newPointDistanceFromStartTr * Random.Range(-0.15f, 1.0f) * _startTr.up); // Y (�Ʒ��� ����, ���� ��ü)

        // ���� ������ �������� ���� ����Ʈ ����
        m_points[2] = _endTr.position +
            (_newPointDistanceFromEndTr * Random.Range(-1.0f, 1.0f) * _endTr.right) + // X (��, �� ��ü)
            (_newPointDistanceFromEndTr * Random.Range(-1.0f, 1.0f) * _endTr.up); // Y (��, �Ʒ� ��ü)

        // ���� ����
        m_points[3] = _endTr.position;

        transform.position = _startTr.position;
    }

    void Update()
    {
        //Ÿ�̸��� �ð��� ������ ����
        if (m_timerCurrent > m_timerMax)
        {
            return;
        }

        // ��� �ð� ����ϱ�
        m_timerCurrent += Time.deltaTime * m_speed;

        // ������ ����� X,Y ��ǥ ���
            transform.position = new Vector2(
            CubicBezierCurve(m_points[0].x, m_points[1].x, m_points[2].x, m_points[3].x),
            CubicBezierCurve(m_points[0].y, m_points[1].y, m_points[2].y, m_points[3].y)
        );
    }

    // �ݶ��̴� �浹 �� ������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.transform.CompareTag("BossMonster") || collision.transform.CompareTag("Monster"))
        //{
        //    collision.gameObject.GetComponent<MonsterManager>().UnderAttack(10, hiteffect);
        //}
    }

    /// <summary>
    /// 3�� ������ �.
    /// </summary>
    /// <param name="a">���� ��ġ</param>
    /// <param name="b">���� ��ġ���� �󸶳� ���� �� ���ϴ� ��ġ</param>
    /// <param name="c">���� ��ġ���� �󸶳� ���� �� ���ϴ� ��ġ</param>
    /// <param name="d">���� ��ġ</param>
    /// <returns></returns>

    private float CubicBezierCurve(float a, float b, float c, float d)
    {
        // (0~1)�� ���� ���� ������ � ���� ���ϱ� ������, ������ ���� �ð��� ���ߴ�.
        float t = m_timerCurrent / m_timerMax; // (���� ��� �ð� / �ִ� �ð�)

        // ������.
        /*
        return Mathf.Pow((1 - t), 3) * a
            + Mathf.Pow((1 - t), 2) * 3 * t * b
            + Mathf.Pow(t, 2) * 3 * (1 - t) * c
            + Mathf.Pow(t, 3) * d;
        */

        float ab = Mathf.Lerp(a, b, t);         //a�� b������ ��ǥ ���ϱ�
        float bc = Mathf.Lerp(b, c, t);         //b�� c������ ��ǥ ���ϱ�
        float cd = Mathf.Lerp(c, d, t);         //d�� d������ ��ǥ ���ϱ�

        float abbc = Mathf.Lerp(ab, bc, t);     //ab�� bc������ ��ǥ ���ϱ�
        float bccd = Mathf.Lerp(bc, cd, t);     //bc�� cd������ ��ǥ ���ϱ�

        return Mathf.Lerp(abbc, bccd, t);
    }
}
