using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum Transition//条件转换的枚举类型
//{
//    NullTransition = 0,
//    Warning,//1
//    Boring,//2
//    SawPlayer,//3
//    Dead//4
//}

//public enum StateID//为状态加入枚举标签
//{
//    NullStateID = 0,
//    Guard,//1
//    Patrol,//2
//    Chase,//3
//    Dead//4
//}

/// <summary>
/// 状态基类接口
/// 这个类代表状态在有限状态机系统中
/// 每个状态都有一个由一对搭档（过渡-状态）组成的字典来表示当前状态下如果一个过渡被触发状态机会进入那个状态
///  Reason方法被用来决定那个过渡会被触发
/// Act方法来表现NPC出在当前状态的行为
/// </summary>

public abstract class FSMstate
{
    //protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();
    protected StateID stateId;

    public StateID ID
    {
        get{return stateId;}
    }

    //public void AddTransition(Transition transition, StateID id)
    //{
    //    if (transition == Transition.NullTransition)
    //    {
    //        Debug.Log("transition不存在");
    //        return;
    //    }
    //    if (id == StateID.NullStateID)
    //    {
    //        Debug.Log("NullStateID");
    //        return;
    //    }
    //    if (map.ContainsKey(transition))
    //    {
    //        return;
    //    }
    //    map.Add(transition, id);
    //}
    //public void DeleteTransition(Transition transition)
    //{
    //    if (transition == Transition.NullTransition)
    //    {
    //        return;
    //    }
    //    if (map.ContainsKey(transition))
    //    {
    //        map.Remove(transition);
    //        return;
    //    }
    //    Debug.LogError("不存在transition");
    //}
    /// <summary>
    ///  该方法在该状态接收到一个过渡时返回状态机需要成为的新状态
    /// </summary>
    //public StateID GetOutputState(Transition transition)
    //{
    //    Debug.Log(transition);
    //    if (map.ContainsKey(transition))
    //    {
    //        return map[transition];
    //    }

    //    return StateID.NullStateID;
    //}
    public virtual void DoBeforeEnter() { }
    public virtual void DoBeforeLeaving() { }
    public abstract void Reason(GameObject player, GameObject target);
    public abstract void Act(GameObject player, GameObject target);
}
