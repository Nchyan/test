using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyState))]
public class EnemyController : MonoBehaviour,IEndGameObserver
{
    public GameObject player;
    public GameObject thisEnemy;

    public GameObject atkTarget;//攻击的目标

    public EnemyState enemyState;

    public Collider coll;

    public Vector3 guardPos;
    public Quaternion guardRotation;

    private FSMsystem fsm;
    public NavMeshAgent agent;
    public Animator anim;

    [Header("Base setting")]
    public float sightRadius = 10f;

    public bool isStay;

    public StateID originState;

    public bool anim_walk;
    public bool anim_chase;
    public bool anim_follow;
    public bool anim_dead;

    public bool playerDead = false;

    [Header("Patrol State")]
    public float patrolRange;
    public float patrolSpeed;
    public float patrolPerTime;//巡逻周期等待间隔
    public Vector3 originalPoint;//初始巡逻点范围

    [Header("Chase State")]
    public float chaseSpeed;
    [Header("Skill")]
    public bool readySkill;

    void OnEnable()
    {
        GameManager.Instance.AddObserver(this);
    }
    void OnDisable()
    {
        if (!GameManager.IsInitialized)
            return;

        if (GetComponent<LootSpawner>() && anim_dead)
            GetComponent<LootSpawner>().spawnLoot();

        if (GameManager.IsInitialized && anim_dead)//怪物死亡时，更新任务状态
            QuestManager.Instance.UpdateQuestProcess(this.name, 1);
        //print(this.name);

        GameManager.Instance.RemoveObserver(this);
    }

    void Start()
    {
        originalPoint = thisEnemy.transform.position;
        guardPos = originalPoint;
        guardRotation = transform.rotation;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider>();
        enemyState = GetComponent<EnemyState>();
        MakeFSM();
        setState(originState);
    }
    
    public void setState(StateID transition)
    {
        fsm.PerformTransition(transition);
    }

    void MakeFSM()//初始化状态机
    {
        Guard guard = new Guard(thisEnemy);
        Chase chase = new Chase(thisEnemy);
        Patrol patrol = new Patrol(thisEnemy);
        Dead dead = new Dead(thisEnemy);
        
        fsm = new FSMsystem();
        fsm.AddState(StateID.Guard,guard);
        fsm.AddState(StateID.Chase,chase);
        fsm.AddState(StateID.Patrol,patrol);
        fsm.AddState(StateID.Dead, dead);
    }
    void FixedUpdate()//update的高性能版
    {
        if (enemyState.enemyData.Hp == 0)
        {
            anim_dead = true;
            anim.SetBool("Death", anim_dead);//设置死亡动画
            setState(StateID.Dead);
        }
        else if(!playerDead)//确认玩家死亡情况
        {
            fsm.CurrentFsmState.Reason(player, thisEnemy);
            fsm.CurrentFsmState.Act(player, thisEnemy);
        }
        
        //coolDownTimer();
        SwitchAnim();
    }

    void SwitchAnim()//更新动画状态
    {
        anim.SetBool("Walk",anim_walk);
        anim.SetBool("Chase", anim_chase);
        anim.SetBool("Follow", anim_follow);
    }

    void Hit()//攻击！
    {
        if (atkTarget != null && transform.IsFacingTarget(atkTarget.transform))
        { 
            var targetStates = enemyState.GetComponent<EnemyState>();
            enemyState.takeDamage(GameManager.Instance.playerStats);
        }        
    }

    void OnDrawGizmosSelected()//unity系统自带的范围线
    {
        //视野范围线
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRadius);

        //巡逻范围线
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, patrolRange);
    }

    public void EndNotify()
    {
        playerDead = true;
        anim.SetBool("Victory", true);
        anim_walk = false;
        anim_chase = false;
        anim_follow = false;
        atkTarget = null;
    }
}
