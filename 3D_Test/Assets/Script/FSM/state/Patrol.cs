using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : FSMstate//巡逻状态
{
    private EnemyController enemyCon;
    private Transform enemyTrans;
    private EnemyState enemyState;

    private float waittingTime;//巡逻周期等待间隔
    private float range;//巡逻范围
    private Vector3 wayPonit;

    public Patrol(GameObject enemy)
    {
        enemyCon = enemy.GetComponent<EnemyController>();
        enemyTrans = enemy.GetComponent<Transform>();
        enemyState = enemy.GetComponent<EnemyState>();
        range = enemyCon.patrolRange;
        waittingTime = enemyCon.patrolPerTime;
        stateId = StateID.Patrol;
        printNewPoint();
    }
    
    public override void DoBeforeLeaving()
    {
        enemyCon.anim_walk = false;
    }
    public override void DoBeforeEnter()
    {
        enemyCon.agent.isStopped = false;
        enemyCon.anim_walk = true;
        enemyCon.agent.speed = enemyCon.patrolSpeed;
    }
    public override void Act(GameObject player, GameObject target)
    {
        if (Vector3.Distance(wayPonit, enemyTrans.position) <= enemyCon.agent.stoppingDistance)//到达巡逻点
        {
            enemyCon.anim_walk = false;
            if (waittingTime < 0)
                printNewPoint();
            else
                waittingTime -= Time.deltaTime;
        }
        else //前往巡逻点
        {
            enemyCon.anim_walk = true;
            enemyCon.agent.destination = wayPonit;
        }
    }

    public override void Reason(GameObject player, GameObject target)
    {
        if (FoundPlayer())
        {
            enemyCon.setState(StateID.Chase);
        }
    }

    private void printNewPoint()
    {
        waittingTime = enemyCon.patrolPerTime;

        float Rx = Random.Range(-range, range);
        float Rz = Random.Range(-range, range);

        Vector3 newPoint = new Vector3(enemyCon.originalPoint.x + Rx, enemyCon.originalPoint.y, enemyCon.originalPoint.z + Rz);

        NavMeshHit hit;
        wayPonit = NavMesh.SamplePosition(newPoint,out hit,range,1)? hit.position: enemyTrans.position;//防止怪物卡墙动不了 
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
}
