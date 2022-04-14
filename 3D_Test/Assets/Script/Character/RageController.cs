using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageController : MonoBehaviour
{
    public RageData_SO playerRage;
    [Header("Rage Setting")]
    public int hitGet;//���ι����������
    public float replyTime = 4f;//ŭ������ʱ��
    public float lastReplyTime = 4f;//ŭ�����˼�ʱ
    public float perReplyTime = 0.5f;//ŭ������Ƶ��
    public float lastPerReplyTime = 0.5f;//ŭ������Ƶ�ʼ�ʱ
    public int perReply = 5;//ŭ��������

    void Awake()
    {
        //playerRage = GameManager.Instance.playerStats.rageData;
    }
    private void Update()
    {
        lastReplyTime -= Time.deltaTime;
        if (lastReplyTime < 0)
        {
            lastPerReplyTime -= Time.deltaTime;
            if (lastPerReplyTime < 0)
            {
                playerRage.Rage = Mathf.Max(playerRage.Rage - perReply, 0);
                lastPerReplyTime = perReplyTime;
            }
        }
    }

    public void hitRage(int atk)
    {
        int get = hitGet + atk / 5;//���ι���ŭ����ʽ��(�̶�ֵ + �����˺���1/5)

        if (playerRage.Rage + get <= playerRage.maxRage)
            playerRage.Rage += get;
        else//��ŭ��
            playerRage.Rage = playerRage.maxRage;

        lastReplyTime = replyTime;
    }
    public void damageRage(int damage)
    {
        int get = damage / 3;//��������ŭ����ʽ��(�����˺���1/3)

        if (playerRage.Rage + get <= playerRage.maxRage)
            playerRage.Rage += get;
        else//��ŭ��
            playerRage.Rage = playerRage.maxRage;

        lastReplyTime = replyTime;
    }
}
