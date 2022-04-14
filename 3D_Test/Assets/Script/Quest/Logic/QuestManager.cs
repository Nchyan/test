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

            if (tempTask != null)//如果任务存在对应目标，更新对应数据
            { 
                tempTask.curAmount += amount;
                task.questData.checkQuestProcess();//检查任务是否被完成
            }
        }
    }

    public bool HaveQuest(QuestData_SO quest)//检查该任务是否已经领取
    {
        if (quest != null)
            return taskList.Any(q => q.questData.questName == quest.questName);//遍历taskList查找是否存在相同questName
        else
            return false;
    }
    public QuestTask GetTask(QuestData_SO quest)//获取对应QuestTask
    {
        return taskList.Find(q => q.questData.questName == quest.questName);
    }
}
