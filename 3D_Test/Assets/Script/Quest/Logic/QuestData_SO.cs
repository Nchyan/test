using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/Quest Data")]
public class QuestData_SO : ScriptableObject
{
    [System.Serializable]
    public class QuestRequire //单项任务需求
    {
        public string aimName;//需求名
        public int aimAmount;//目标数量
        public int curAmount;//当前数量
    }

    public string questName;//任务名
    [TextArea]
    public string description;//任务描述

    //任务状态
    public bool isStart;//进行中
    public bool isComplete;//已完成(达成任务目标未提交)
    public bool isEnd;//已提交

    public List<QuestRequire> requireList = new List<QuestRequire>();//需求列表
    public List<InventoryItem> rewardList = new List<InventoryItem>();//奖励列表

    public void checkQuestProcess()//检查任务是否完成
    {
        var finishCount = requireList.Where(r => r.curAmount >= r.aimAmount);
        isComplete = (bool)(finishCount.Count() == requireList.Count);
    }

    public List<string> getRequireNameList()//获取该任务目标名字的列表
    {
        List<string> list = new List<string>();

        foreach (var req in requireList)
        {
            list.Add(req.aimName);
        }

        return list;
    }

    public void GiveReward()//获取奖励（提交道具）
    {
        foreach (var rew in rewardList)
        {
            if (rew.amount < 0)//提交道具
            {
                int count = Mathf.Abs(rew.amount);//提交数量

                if (InventoryManager.Instance.getQusetItemInBag(rew.itemData) != null)
                {
                    if (InventoryManager.Instance.getQusetItemInBag(rew.itemData).amount <= count)//提交的物品部分在背包里
                    {
                        count -= InventoryManager.Instance.getQusetItemInBag(rew.itemData).amount;
                        InventoryManager.Instance.getQusetItemInBag(rew.itemData).amount = 0;

                        if(InventoryManager.Instance.getQusetItemInAction(rew.itemData) != null)//快捷栏上存在需要提交的物品
                            InventoryManager.Instance.getQusetItemInAction(rew.itemData).amount -= count;
                    }
                    else//提交的物品全在背包里
                    {
                        InventoryManager.Instance.getQusetItemInBag(rew.itemData).amount -= count;
                    }
                }
                else//背包里没有需要提交的物品
                {
                    InventoryManager.Instance.getQusetItemInAction(rew.itemData).amount -= count;
                }
            }
            else
            {
                InventoryManager.Instance.InventoryData.AddItem(rew.itemData, rew.amount);
            }

            InventoryManager.Instance.inventoryUI.printUI();
            InventoryManager.Instance.ActionUI.printUI();
        }
    }
}
