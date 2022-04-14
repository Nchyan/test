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

        if (newItem.stackable)//确认物品可收叠
        {
            foreach (var item in itemList)//检查该物品是否存在于背包
            {
                if (newItem == item.itemData)//找到相同的物品
                {
                    item.amount += num;//堆叠对应数量的物品
                    found = true;
                    return;
                }
            }
        }

        for (int i = 0; i < itemList.Count; ++i) //查找背包的空位
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
