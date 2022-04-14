using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestRequire : MonoBehaviour
{
    public Text aimNameTxt;
    public Text valueTxt;

    public void setRequire(string name,int amount,int curAmount)
    {
        aimNameTxt.text = name;
        valueTxt.text = curAmount.ToString() + " / " + amount.ToString(); 
    }
    public void setRequire(string name,bool finish)
    {
        aimNameTxt.text = name;
        valueTxt.text = "Finish";
    }
}
