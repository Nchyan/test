using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : FSMstate//站桩状态
{
    private EnemyController enemyCon;
    private Transform enemyTrans;
    private EnemyState enemyState;

    public Guard(GameObject enemy)
    {
        enemyCon = enemy.GetComponent<EnemyController>();
        enemyTrans = enemy.GetComponent<Transform>();
        enemyState = enemy.GetComponent<EnemyState>();
        stateId = StateID.Guard;
    }
    public override void DoBeforeLeaving()
    {
        enemyCon.agent.isStopped = false;
    }
    public override void DoBeforeEnter()
    {
        enemyCon.agent.isStopped = true;
        enemyCon.anim_chase = false;
    }
    public override void Act(GameObject player, GameObject target)
    {
        
    }

    public override void Reason(GameObject player, GameObject target)
    {
        if (enemyCon.isStay)//该敌人全程不动
            return;

        if (FoundPlayer())
        {
            enemyCon.setState(StateID.Chase);
        }
        else if (enemyCon.transform.position != enemyCon.guardPos)
        {
            enemyCon.anim_walk = true;
            enemyCon.anim_follow = false;
            enemyCon.agent.isStopped = false;
            enemyCon.agent.destination = enemyCon.guardPos;
            if (Vector3.Distance(enemyTrans.position, enemyCon.guardPos) <= enemyCon.agent.stoppingDistance)
            { 
                enemyCon.anim_walk = false;
                enemyTrans.rotation = Quaternion.Lerp(enemyTrans.rotation, enemyCon.guardRotation, 0.1f);
            }       
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
}
