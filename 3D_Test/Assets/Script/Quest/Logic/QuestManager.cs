using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestManager : Singleton<QuestManager>
{
    [System.Serializable]
    public class QuestTask 
    {
        public QuestData_SO questData;
        public bool IsStart { get { return questData.isStart; } set { questData.isStart = value; } }
        public bool IsComplete { get { return questData.isComplete; } set { questData.isComplete = value; } }
        public bool IsEnd { get { return questData.isEnd; } set { questData.isEnd = value; } }
    }

    public List<QuestTask> taskList = new List<QuestTask>();

    public void UpdateQuestProcess(string aimName, int amount)
    {
        foreach (var task in taskList)
        {
            var tempTask = task.questData.requireList.Find(r => r.aimName == aimName);

            if (tempTask != null)//���������ڶ�ӦĿ�꣬���¶�Ӧ����
            { 
                tempTask.curAmount += amount;
                task.questData.checkQuestProcess();//��������Ƿ����
            }
        }
    }

    public bool HaveQuest(QuestData_SO quest)//���������Ƿ��Ѿ���ȡ
    {
        if (quest != null)
            return taskList.Any(q => q.questData.questName == quest.questName);//����taskList�����Ƿ������ͬquestName
        else
            return false;
    }
    public QuestTask GetTask(QuestData_SO quest)//��ȡ��ӦQuestTask
    {
        return taskList.Find(q => q.questData.questName == quest.questName);
    }
}
