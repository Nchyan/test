using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestItemButton : MonoBehaviour
{
    public Text QuestNameTxt;
    public Text QuestContentTxt;
    public Image Light;

    public QuestData_SO curData;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(printTask);
    }

    void printTask()
    {
        QuestNameTxt.text = curData.questName;
        QuestContentTxt.text = curData.description;
        QuestUI.Instance.printRequire(curData);
        QuestUI.Instance.printReward(curData);
        
    }

    public void setButton(QuestData_SO questData)
    {
        curData = questData;

        if (questData.isComplete == true)//更新完成状态显示灯
            Light.color = new Color(255f/255, 255f/255, 0);
        if (questData.isEnd == true)//更新结束状态显示灯
            Light.color = new Color(0, 255f/255, 0);

        QuestNameTxt.text = questData.questName;
    }
}
