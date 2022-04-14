using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apply_recover : MonoBehaviour,IApplyFunction
{
    public int value;//»Ø¸´Á¿;
    
    public void Apply()
    {
        GameManager.Instance.playerStats.applyHealth(value);
    }
}
