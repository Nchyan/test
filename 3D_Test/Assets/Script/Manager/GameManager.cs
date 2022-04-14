using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayerState playerStats;

    public List<IEndGameObserver> endGameObservers = new List<IEndGameObserver>();

    public void RigisterPlayer(PlayerState player)
    {
        playerStats = player;
    }
    public void AddObserver(IEndGameObserver ob)
    {
        endGameObservers.Add(ob);
    }
    public void RemoveObserver(IEndGameObserver ob)
    {
        endGameObservers.Remove(ob);
    }
    public void NotifyObservers()
    {
        foreach (var ob in endGameObservers)
        {
            ob.EndNotify();
        }
    }

    public Transform GetEntrance()
    {
        var entrances = FindObjectsOfType<TransitionDestination>();

        foreach (var des in entrances)
        {
            if (des.tag == TransitionDestination.DestinationTag.Enter)
                return des.transform;
        }
        return null;
    }
}
