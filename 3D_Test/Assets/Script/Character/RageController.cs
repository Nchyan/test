using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageController : MonoBehaviour
{
    public RageData_SO playerRage;
    [Header("Rage Setting")]
    public int hitGet;//单次攻击基础获得
    public float replyTime = 4f;//怒气消退时间
    public float lastReplyTime = 4f;//怒气消退计时
    public float perReplyTime = 0.5f;//怒气消退频率
    public float lastPerReplyTime = 0.5f;//怒气消退频率计时
    public int perReply = 5;//怒气消退量

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
        int get = hitGet + atk / 5;//单次攻击怒气公式：(固定值 + 单次伤害的1/5)

        if (playerRage.Rage + get <= playerRage.maxRage)
            playerRage.Rage += get;
        else//满怒气
            playerRage.Rage = playerRage.maxRage;

        lastReplyTime = replyTime;
    }
    public void damageRage(int damage)
    {
        int get = damage / 3;//单次受伤怒气公式：(单次伤害的1/3)

        if (playerRage.Rage + get <= playerRage.maxRage)
            playerRage.Rage += get;
        else//满怒气
            playerRage.Rage = playerRage.maxRage;

        lastReplyTime = replyTime;
    }
}
