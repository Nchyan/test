using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GruntController : EnemyController
{
    private float lastSkillTime;
    [Header("Skill")]
    public float SkillCD;
    public int kickForce;
    public int SkillRange;

    void Update()
    {
        lastSkillTime -= Time.deltaTime;
        if (InSkillRange())
        {
            if (lastSkillTime <= 0)
            {
                readySkill = true;
                lastSkillTime = SkillCD;
            }
        }
    }

    public void KickOff()
    {
        if (atkTarget != null && transform.IsFacingTarget(atkTarget.transform))
        {
            transform.LookAt(atkTarget.transform);

            Vector3 dir = atkTarget.transform.position - transform.position;
            dir.Normalize();
            atkTarget.GetComponent<NavMeshAgent>().isStopped = true;
            atkTarget.GetComponent<NavMeshAgent>().velocity = dir * kickForce;
            atkTarget.GetComponent<Animator>().SetTrigger("Dizzy");
        }
    }
    private bool InSkillRange()//是否进入技能范围
    {
        if (atkTarget != null)
            return Vector3.Distance(transform.position, atkTarget.transform.position) <= SkillRange;
        return false;
    }
}
