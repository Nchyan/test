using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStateUI : MonoBehaviour
{
    public Image hpSilder;
    public Image rageSilder;

    void Update()
    {
        printHp();
        printRage();
    }

    private void printHp()
    {
        float per = (float)GameManager.Instance.playerStats.playerData.Hp / GameManager.Instance.playerStats.playerData.maxHp;
        hpSilder.fillAmount = per;
    }
    private void printRage()
    {
        float per = (float)GameManager.Instance.playerStats.rageData.Rage / GameManager.Instance.playerStats.rageData.maxRage;
        rageSilder.fillAmount = per;
    }
}
