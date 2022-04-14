using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : FSMstate//追击状态
{
    private EnemyController enemyCon;
    private Transform enemyTrans;
    private EnemyState enemyState;
    private float waittingTime;//脱离视野后的等待时间
    private float lastAtkTime;//攻击cd

    public Chase(GameObject enemy)
    {
        enemyCon = enemy.GetComponent<EnemyController>();
        enemyTrans = enemy.GetComponent<Transform>();
        enemyState = enemy.GetComponent<EnemyState>();
        waittingTime = enemyCon.patrolPerTime;
        stateId = StateID.Chase;
    }
    
    public override void DoBeforeLeaving()
    {
        enemyCon.anim_follow = false;
        enemyCon.anim_chase = false;
    }
    public override void DoBeforeEnter()
    {
        enemyCon.agent.isStopped = false;
        enemyCon.anim_follow = true;
        enemyCon.anim_chase = true;
        enemyCon.agent.speed = enemyCon.chaseSpeed;
    }
    public override void Act(GameObject player, GameObject target)
    {
        if (enemyCon.atkTarget != null)//追击攻击的目标
            enemyCon.agent.destination = enemyCon.atkTarget.transform.position;
    }

    public override void Reason(GameObject player, GameObject target)
    {
        lastAtkTime -= Time.deltaTime;
        if (!FoundPlayer())
        {
            enemyCon.anim_chase = false;
            enemyCon.agent.isStopped = true;
            if (waittingTime < 0) 
                enemyCon.setState(enemyCon.originState); 
            else 
                waittingTime -= Time.deltaTime;
        }
        else
        {
            enemyCon.anim_chase = true;
            enemyCon.agent.isStopped = false;
            waittingTime = enemyCon.patrolPerTime;//重置脱战等待计时器
        }

        if (enemyCon.readySkill)//若达成技能施放条件，则使用技能
        {
            enemyCon.anim_follow = false;
            enemyCon.agent.isStopped = true;            
            enemyCon.anim.SetTrigger("Skill");
            enemyCon.readySkill = false;
        }
        else if (InAttackRange())//若在攻击范围内则进行攻击
        {
            enemyCon.anim_follow = false;
            enemyCon.agent.isStopped = true;
            if (lastAtkTime < 0)
            {
                enemyCon.anim.SetTrigger("Attack");
                lastAtkTime = enemyState.atkSpeed;
            }
        }
        else
        {
            enemyCon.anim_follow = true;
            enemyCon.agent.isStopped = false;
        }
    }  

    private bool FoundPlayer()
    {
        var colliders = Physics.OverlapSphere(enemyTrans.position, enemyCon.sightRadius);

        foreach (var target in colliders)
        {
            if (target.CompareTag("Player")) 
            {
                enemyCon.atkTarget = target.gameObject;
                return true;
            }
        }
        enemyCon.atkTarget = null;
        return false;
    }

    private bool InAttackRange()//是否进入攻击范围
    {
        if (enemyCon.atkTarget != null)
            return Vector3.Distance( enemyTrans.position , enemyCon.atkTarget.transform.position) <= enemyState.atkRange;
        return false;    
    }
}
