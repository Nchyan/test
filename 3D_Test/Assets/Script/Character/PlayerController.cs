using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private PlayerState playerState;//人物状态

    private GameObject attackTarget;

    private bool anim_dead = false;

    private float lastAtkTime;
    private float stopDistance;
    // Start is called before the first frame update
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        playerState = GetComponent<PlayerState>();
        lastAtkTime = playerState.atkSpeed;

        stopDistance = agent.stoppingDistance;
    }
    private void OnEnable()
    {
        MouseManager.Instance.onMouseClicked += EventToMove;//点击鼠标
        MouseManager.Instance.onEnemyClicked += EventToAttack;//点击敌人

        GameManager.Instance.RigisterPlayer(playerState);
    }
    void Start()
    {
        SaveManager.Instance.LoadPlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState.Hp == 0)//人物死亡
            Dead();

        SwitchMoveAnim();
        lastAtkTime -= Time.deltaTime;
    }

    public void SwitchMoveAnim()
    {
        anim.SetFloat("speed", agent.velocity.sqrMagnitude);
        anim.SetBool("Death", anim_dead);
    }

    public void EventToMove(Vector3 target)//角色移动
    {
        StopAllCoroutines();
        if (anim_dead) return;

        agent.stoppingDistance = stopDistance;
        agent.isStopped = false;
        agent.destination = target;
    }
    public void EventToAttack(GameObject target)//走向选中的敌人
    {
        if (anim_dead) return;
        if (target != null) 
        {
            attackTarget = target;
            StartCoroutine(MoveToAttack());
        }
    }

    IEnumerator MoveToAttack()
    {
        agent.isStopped = false;
        agent.stoppingDistance = playerState.atkData.atkRange;

        transform.LookAt(attackTarget.transform);
        while (Vector3.Distance(transform.position,attackTarget.transform.position) >= playerState.atkRange)
        {
            agent.destination = attackTarget.transform.position;
            yield return null;
        }
        agent.isStopped = true;//停止角色移动

        if (lastAtkTime < 0)
        {
            anim.SetTrigger("attack");
            lastAtkTime = playerState.atkSpeed;
        }
    }

    void Hit()//狗头人挥剑
    {
        var targetStates = attackTarget.GetComponent<EnemyState>();
        playerState.takeDamage(targetStates);
    }

    void Dead()//人物去世
    {
        anim_dead = true;
        GameManager.Instance.NotifyObservers();
    }
}
