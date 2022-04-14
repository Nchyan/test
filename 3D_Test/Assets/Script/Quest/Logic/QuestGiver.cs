using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueController))]
public class QuestGiver : MonoBehaviour
{
    DialogueController controller;
    QuestData_SO curQuest;

    public DialogueData_SO startDialogue;//��ȡ����ǰ�ĶԻ�
    public DialogueData_SO progressDialogue;//��������еĶԻ�
    public DialogueData_SO completeDialogue;//�������ʱ�ĶԻ�
    public DialogueData_SO endDialogue;//��������ĶԻ�

    public bool IsStart 
    {
        get
        {
            if (QuestManager.Instance.HaveQuest(curQuest))
            {
                return QuestManager.Instance.GetTask(curQuest).IsStart;
            }
            else return false;
        }
    }
    public bool IsComplete
    {
        get
        {
            if (QuestManager.Instance.HaveQuest(curQuest))
            {
                return QuestManager.Instance.GetTask(curQuest).IsComplete;
            }
            else return false;
        }
    }
    public bool IsEnd
    {
        get
        {
            if (QuestManager.Instance.HaveQuest(curQuest))
            {
                return QuestManager.Instance.GetTask(curQuest).IsEnd;
            }
            else return false;
        }
    }
    void Awake()
    {
        controller = GetComponent<DialogueController>();
    }
    void Start()
    {
        controller.DialogueData = startDialogue;
        curQuest = controller.DialogueData.getQuest();//��ȡ��ǰ�Ի��������
    }
    void Update()
    {
        if (IsStart)
        {
            if (IsComplete)//�����Ƿ����
                controller.DialogueData = completeDialogue;//�������ʱ�ĶԻ�
            else
                controller.DialogueData = progressDialogue;//��������еĶԻ�
        }
        if (IsEnd)//
        {
            controller.DialogueData = endDialogue;
        }
    }
}
