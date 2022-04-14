using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    string sceneName = "";
    public string SceneName { get { return PlayerPrefs.GetString(sceneName); }  }
    protected void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Save(Object data, string key)//存储数据
    {
        var jsondata = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, jsondata);
        PlayerPrefs.SetString(key, jsondata);
        PlayerPrefs.Save();
    }

    public void Load(Object data, string key)//读取数据
    {
        if (PlayerPrefs.HasKey(key))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key),data);
        }
    }

    public void SavePlayerData()
    {
        Save(GameManager.Instance.playerStats.playerData,"playerBaseData");
        Save(GameManager.Instance.playerStats.atkData, "playerAtkData");
    }

    public void LoadPlayerData()
    {
        Load(GameManager.Instance.playerStats.playerData, "playerBaseData");
        
    }
}
