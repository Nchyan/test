using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/Quest Data")]
public class QuestData_SO : ScriptableObject
{
    [System.Serializable]
    public class QuestRequire //������������
    {
        public string aimName;//������
        public int aimAmount;//Ŀ������
        public int curAmount;//��ǰ����
    }

    public string questName;//������
    [TextArea]
    public string description;//��������

    //����״̬
    public bool isStart;//������
    public bool isComplete;//�����(�������Ŀ��δ�ύ)
    public bool isEnd;//���ύ

    public List<QuestRequire> requireList = new List<QuestRequire>();//�����б�
    public List<InventoryItem> rewardList = new List<InventoryItem>();//�����б�

    public void checkQuestProcess()//��������Ƿ����
    {
        var finishCount = requireList.Where(r => r.curAmount >= r.aimAmount);
        isComplete = (bool)(finishCount.Count() == requireList.Count);
    }

    public List<string> getRequireNameList()//��ȡ������Ŀ�����ֵ��б�
    {
        List<string> list = new List<string>();

        foreach (var req in requireList)
        {
            list.Add(req.aimName);
        }

        return list;
    }

    public void GiveReward()//��ȡ�������ύ���ߣ�
    {
        foreach (var rew in rewardList)
        {
            if (rew.amount < 0)//�ύ����
            {
                int count = Mathf.Abs(rew.amount);//�ύ����

                if (InventoryManager.Instance.getQusetItemInBag(rew.itemData) != null)
                {
                    if (InventoryManager.Instance.getQusetItemInBag(rew.itemData).amount <= count)//�ύ����Ʒ�����ڱ�����
                    {
                        count -= InventoryManager.Instance.getQusetItemInBag(rew.itemData).amount;
                        InventoryManager.Instance.getQusetItemInBag(rew.itemData).amount = 0;

                        if(InventoryManager.Instance.getQusetItemInAction(rew.itemData) != null)//������ϴ�����Ҫ�ύ����Ʒ
                            InventoryManager.Instance.getQusetItemInAction(rew.itemData).amount -= count;
                    }
                    else//�ύ����Ʒȫ�ڱ�����
                    {
                        InventoryManager.Instance.getQusetItemInBag(rew.itemData).amount -= count;
                    }
                }
                else//������û����Ҫ�ύ����Ʒ
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
