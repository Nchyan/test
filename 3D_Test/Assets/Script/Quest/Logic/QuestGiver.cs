using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueController))]
public class QuestGiver : MonoBehaviour
{
    DialogueController controller;
    QuestData_SO curQuest;

    public DialogueData_SO startDialogue;//领取任务前的对话
    public DialogueData_SO progressDialogue;//任务进行中的对话
    public DialogueData_SO completeDialogue;//完成任务时的对话
    public DialogueData_SO endDialogue;//完成任务后的对话

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
        curQuest = controller.DialogueData.getQuest();//获取当前对话里的任务
    }
    void Update()
    {
        if (IsStart)
        {
            if (IsComplete)//任务是否完成
                controller.DialogueData = completeDialogue;//任务完成时的对话
            else
                controller.DialogueData = progressDialogue;//任务进行中的对话
        }
        if (IsEnd)//
        {
            controller.DialogueData = endDialogue;
        }
    }
}
