using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    public GameObject playerPrefab;
    GameObject player;

    NavMeshAgent playerAgent;

    public void TransToDestination(TransitionPoint TransPoint)
    {
        switch (TransPoint.type)
        {
            case TransitionPoint.TransType.SameScene:
                StartCoroutine(Transition(SceneManager.GetActiveScene().name, TransPoint.destination));
                break;
            case TransitionPoint.TransType.DifferentScene:
                break;
        }        
    }
    public void TransToLoadGame()
    {
        //StartCoroutine(Loadlevel(SaveManager.Instance.SceneName));
    }
    public void TransToFirstLevel()
    {
        StartCoroutine(Loadlevel("SampleScene"));
    }

    IEnumerator Transition(string SceneName, TransitionDestination.DestinationTag destinationTag)
    {
        player = GameManager.Instance.playerStats.gameObject;
        playerAgent = player.GetComponent<NavMeshAgent>();
        playerAgent.enabled = false;
        player.transform.SetPositionAndRotation(GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);
        playerAgent.enabled = true;
        yield return null;
    }

    private TransitionDestination GetDestination(TransitionDestination.DestinationTag destinationTag)
    {
        var entrances = FindObjectsOfType<TransitionDestination>();

        foreach (var des in entrances)
        {
            if (des.tag == destinationTag)
                return des;
        }
        return null;
    }

    IEnumerator Loadlevel(string scene)
    {
        if (scene != "" && scene != null)
        {
            yield return SceneManager.LoadSceneAsync(scene);
            //yield return player = Instantiate(playerPrefab, GameManager.Instance.GetEntrance());

            SaveManager.Instance.SavePlayerData();

            yield break;
        }
    }
}
