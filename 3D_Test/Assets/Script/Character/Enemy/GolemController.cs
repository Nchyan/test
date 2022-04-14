using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemController : EnemyController
{
    private float lastSkillTime;
    private float lastAtkTime;
    [Header("Skill")]
    public float SkillCD;
    public int kickForce;
    public int SkillRange;
    [Header("Prefab")]
    public GameObject rockPrefab;
    public Transform handPos;

    void Update()
    {
        lastSkillTime -= Time.deltaTime;
        lastAtkTime -= Time.deltaTime;
        if (InSkillRange())
        {
            if (lastSkillTime <= 0)
            {
                readySkill = true;
                lastSkillTime = SkillCD;
            }
        }
        if (readySkill)//����ɼ���ʩ����������ʹ�ü���
        {
            agent.isStopped = true;
            anim.SetTrigger("Skill");
            readySkill = false;
        }
        else if (InAttackRange())//���ڹ�����Χ������й���
        {
            agent.isStopped = true;
            if (lastAtkTime < 0)
            {
                anim.SetTrigger("Attack");
                lastAtkTime = enemyState.atkSpeed;
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
    public void throwRock()
    {
        if (atkTarget != null)
        {
            var rock = Instantiate(rockPrefab, handPos.position,Quaternion.identity);
            rock.GetComponent<Rock>().target = atkTarget;
        }
    }
    private bool InSkillRange()//�Ƿ���뼼�ܷ�Χ
    {
        if (atkTarget != null)
            return Vector3.Distance(transform.position, atkTarget.transform.position) <= SkillRange;
        return false;
    }
    private bool InAttackRange()//�Ƿ���빥����Χ
    {
        if (atkTarget != null)
            return Vector3.Distance(transform.position, atkTarget.transform.position) <= enemyState.atkRange;
        return false;
    }
}
