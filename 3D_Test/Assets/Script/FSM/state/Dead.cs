using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : FSMstate//ËÀÍö×´Ì¬
{
    private EnemyController enemyCon;

    public Dead(GameObject enemy)
    {
        enemyCon = enemy.GetComponent<EnemyController>();        
        stateId = StateID.Dead;
    }

    public override void DoBeforeLeaving()
    {

    }
    public override void DoBeforeEnter()
    {
        enemyCon.coll.enabled = false;
        enemyCon.agent.radius = 0;
        Object.Destroy(enemyCon.thisEnemy, 2f);
    }

    public override void Act(GameObject player, GameObject target)
    { }

    public override void Reason(GameObject player, GameObject target)
    { }
}
