using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StateID//条件转换的枚举类型
{
    NullStateID = 0,
    Guard,//1
    Patrol,//2
    Chase,//3
    Dead//4
}

public class FSMsystem
{
    public Dictionary<StateID, FSMstate> map;

    private StateID currentStateID;
    public StateID CurrentStateID
    {
        get{return currentStateID;}
    }

    private FSMstate currentFsmState;
    public FSMstate CurrentFsmState
    {
        get{return currentFsmState;}
    }
    public FSMsystem()
    {
        map = new Dictionary<StateID, FSMstate>();
    }
    public void AddState(StateID id,FSMstate state)//添加状态进stateList
    {
        if (state == null)
        {
            Debug.LogError("FSM ERROR: Null reference is not allowed");
        }

        if (map.Count == 0)
        {
            map.Add(id,state);

            currentFsmState = state;
            currentStateID = state.ID;
            return;
        }

        foreach (KeyValuePair<StateID,FSMstate> s in map)//检查状态是否重复
        {
            if (s.Value.ID == state.ID)
            {
                return;
            }
        }
        map.Add(id,state);
    }
    public void DeleteState(StateID id)
    {
        if (id == StateID.NullStateID)
        {
            return;
        }

        foreach (KeyValuePair<StateID, FSMstate> s in map)
        {
            if (s.Key == id)
            {
                map.Remove(id);
                return;
            }
        }
        Debug.LogError("FSM ERROR: Impossible to delete state " + id.ToString() + ". It was not on the list of states");
    }
    public void PerformTransition(StateID transition)
    {
        if (transition == StateID.NullStateID)
        {
            return;
        }
        StateID id = currentFsmState.ID;
        if (id == StateID.NullStateID)
        {
            return;
        }
        currentStateID = transition;
        foreach (KeyValuePair<StateID, FSMstate> s in map)
        {
            if (s.Key == currentStateID)
            {
                currentFsmState.DoBeforeLeaving();
                currentFsmState = s.Value;
                currentFsmState.DoBeforeEnter();
                break;
            }
        }
    }
}
