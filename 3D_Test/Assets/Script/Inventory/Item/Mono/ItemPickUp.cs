using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemData_SO itemData;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.Instance.InventoryData.AddItem(itemData, itemData.amount);
            InventoryManager.Instance.inventoryUI.printUI();

            QuestManager.Instance.UpdateQuestProcess(itemData.itemName,itemData.amount);//检测捡起道具的任务条件

            Destroy(gameObject);
        }
    }
}
