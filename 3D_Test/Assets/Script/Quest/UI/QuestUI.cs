using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : Singleton<QuestUI>
{
    bool isOpen = false;

    [Header("Element")]
    public GameObject questPanel;
    public ItemTooltip tooltip;
    public GameObject titleGo;//显示框里"奖励"字段的gameObject
    public GameObject finishImg;//提示任务完成的图片

    [Header("QuestItem")]
    public RectTransform QuestListTrans;
    public QuestItemButton tempQuestBtn;

    [Header("mainElement")]
    public Text QuestTitleTxt;
    public Text QuestDescriptionTxt;

    [Header("requireElement")]
    public RectTransform requireTrans;
    public QuestRequire tempRequire;

    [Header("rewardElement")]
    public RectTransform rewardTrans;
    public ItemUI tempReward;
    

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))//快捷键Q打开关闭任务UI
            opclQuestUI();
    }

    public bool ifOpen()
    {
        return isOpen;
    }

    public void opclQuestUI()//打开或关闭QuestUI
    {
        titleGo.SetActive(false);
        isOpen = !isOpen;
        questPanel.SetActive(isOpen);
        QuestTitleTxt.text = "";//置空任务标题
        QuestDescriptionTxt.text = "";//置空任务内容

        if (isOpen)//打开窗口
            setAllQuest();
        else//关闭窗口
            tooltip.gameObject.SetActive(false);
    }
    public void setAllQuest()
    {
        //置空列表中的全部内容
        foreach (Transform item in QuestListTrans)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in requireTrans)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in rewardTrans)
        {
            Destroy(item.gameObject);
        }

        //载入对应内容
        foreach (var task in QuestManager.Instance.taskList)
        {
            var newTask = Instantiate(tempQuestBtn, QuestListTrans);
            newTask.setButton(task.questData);
            newTask.QuestNameTxt = QuestTitleTxt;
            newTask.QuestContentTxt = QuestDescriptionTxt;
        }
    }

    public void printRequire(QuestData_SO questData)//读取显示单个任务目标内容
    {
        foreach (Transform item in requireTrans)//清空目标列表
        {
            Destroy(item.gameObject);
        }
        foreach (var req in questData.requireList)//读取任务目标
        {
            var q = Instantiate(tempRequire, requireTrans);
            if (questData.isEnd)
            {
                finishImg.SetActive(true);
                q.setRequire(req.aimName, true);
            }
            else
            {
                finishImg.SetActive(false);
                q.setRequire(req.aimName, req.aimAmount, req.curAmount);
            }                
        }
    }
    public void printReward(QuestData_SO questData)//读取显示全部任务奖励
    {
        titleGo.SetActive(true);
        foreach (Transform item in rewardTrans)//清空目标列表
        {
            Destroy(item.gameObject);
        }
        foreach (var rew in questData.rewardList)//读取任务目标
        {
            var item = Instantiate(tempReward, rewardTrans);
            item.setItemUI(rew.itemData, rew.amount);
        }        
    }
}