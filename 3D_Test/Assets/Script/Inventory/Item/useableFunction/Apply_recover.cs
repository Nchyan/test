using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apply_recover : MonoBehaviour,IApplyFunction
{
    public int value;//�ظ���;
    
    public void Apply()
    {
        GameManager.Instance.playerStats.applyHealth(value);
    }
}
