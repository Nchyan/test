using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New InventoryData", menuName = "Inventory/Data")]
public class InventoryData_SO : ScriptableObject
{
    public List<InventoryItem> itemList = new List<InventoryItem>();

    public void AddItem(ItemData_SO newItem,int num)
    {
        bool found = false;

        if (newItem.stackable)//ȷ����Ʒ���յ�
        {
            foreach (var item in itemList)//������Ʒ�Ƿ�����ڱ���
            {
                if (newItem == item.itemData)//�ҵ���ͬ����Ʒ
                {
                    item.amount += num;//�ѵ���Ӧ��������Ʒ
                    found = true;
                    return;
                }
            }
        }

        for (int i = 0; i < itemList.Count; ++i) //���ұ����Ŀ�λ
        {
            if (itemList[i].itemData == null && !found)
            {
                itemList[i].itemData = newItem;
                itemList[i].amount = num;
                break;
            }
        }
    }
}

[System.Serializable]
public class InventoryItem
{
    public ItemData_SO itemData;
    public int amount;
}
