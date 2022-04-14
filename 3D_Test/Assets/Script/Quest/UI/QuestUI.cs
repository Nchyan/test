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
    public GameObject titleGo;//��ʾ����"����"�ֶε�gameObject
    public GameObject finishImg;//��ʾ������ɵ�ͼƬ

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
        if (Input.GetKeyDown(KeyCode.Q))//��ݼ�Q�򿪹ر�����UI
            opclQuestUI();
    }

    public bool ifOpen()
    {
        return isOpen;
    }

    public void opclQuestUI()//�򿪻�ر�QuestUI
    {
        titleGo.SetActive(false);
        isOpen = !isOpen;
        questPanel.SetActive(isOpen);
        QuestTitleTxt.text = "";//�ÿ��������
        QuestDescriptionTxt.text = "";//�ÿ���������

        if (isOpen)//�򿪴���
            setAllQuest();
        else//�رմ���
            tooltip.gameObject.SetActive(false);
    }
    public void setAllQuest()
    {
        //�ÿ��б��е�ȫ������
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

        //�����Ӧ����
        foreach (var task in QuestManager.Instance.taskList)
        {
            var newTask = Instantiate(tempQuestBtn, QuestListTrans);
            newTask.setButton(task.questData);
            newTask.QuestNameTxt = QuestTitleTxt;
            newTask.QuestContentTxt = QuestDescriptionTxt;
        }
    }

    public void printRequire(QuestData_SO questData)//��ȡ��ʾ��������Ŀ������
    {
        foreach (Transform item in requireTrans)//���Ŀ���б�
        {
            Destroy(item.gameObject);
        }
        foreach (var req in questData.requireList)//��ȡ����Ŀ��
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
    public void printReward(QuestData_SO questData)//��ȡ��ʾȫ��������
    {
        titleGo.SetActive(true);
        foreach (Transform item in rewardTrans)//���Ŀ���б�
        {
            Destroy(item.gameObject);
        }
        foreach (var rew in questData.rewardList)//��ȡ����Ŀ��
        {
            var item = Instantiate(tempReward, rewardTrans);
            item.setItemUI(rew.itemData, rew.amount);
        }        
    }
}