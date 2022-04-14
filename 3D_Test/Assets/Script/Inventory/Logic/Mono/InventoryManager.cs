using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager>
{
    bool isOpen = false;

    public class DragData 
    {
        public ItemSlot originSlot;
        public RectTransform originTrans;
    }
    [Header("Inventory")]
    public InventoryData_SO InventoryData;
    public InventoryData_SO ActionData;
    public InventoryData_SO EquipmentData;

    [Header("Container")]
    public ContainerUI inventoryUI;
    public ContainerUI ActionUI;
    public ContainerUI EquipmentUI;

    [Header("Drag")]
    public Canvas DragCanvas;
    public DragData curDrag;
    public ItemSlot DragSlot;

    [Header("UI")]
    public GameObject bagGO;
    public GameObject stateGO;

    [Header("StateText")]
    public Text HpTxt;
    public Text maxHpTxt;
    public Text maxAtkTxt;
    public Text minAtkTxt;
    public Text AcTxt;

    [Header("ItemTooltip")]
    public ItemTooltip tooltip;

    void Start()
    {
        inventoryUI.printUI();
        ActionUI.printUI();
        EquipmentUI.printUI();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))//打开背包快捷键监测
            opclInventoryUI();
        if(isOpen)
            updateStateText();
    }
    public bool IfOpen()
    {
        return isOpen;
    }

    public void opclInventoryUI()
    {
        isOpen = !isOpen;
        bagGO.SetActive(isOpen);
        stateGO.SetActive(isOpen);
    }
    public void updateStateText() //更新状态数值
    {
        HpTxt.text = GameManager.Instance.playerStats.Hp.ToString();
        maxHpTxt.text = GameManager.Instance.playerStats.maxHp.ToString();
        maxAtkTxt.text = GameManager.Instance.playerStats.maxDamage.ToString();
        minAtkTxt.text = GameManager.Instance.playerStats.minDamage.ToString();
        AcTxt.text = GameManager.Instance.playerStats.Ac.ToString();
    }

    public bool checkInInventory(Vector3 mousePos)
    {
        for (int i = 0;i < inventoryUI.slotList.Length; ++i)
        {
            RectTransform t = (RectTransform)inventoryUI.slotList[i].transform;

            if (RectTransformUtility.RectangleContainsScreenPoint(t,mousePos))
            {
                DragSlot = inventoryUI.slotList[i];
                return true;
            }
        }
        return false;
    }
    public bool checkInAction(Vector3 mousePos)
    {
        for (int i = 0; i < ActionUI.slotList.Length; ++i)
        {
            RectTransform t = (RectTransform)ActionUI.slotList[i].transform;

            if (RectTransformUtility.RectangleContainsScreenPoint(t, mousePos))
            {
                DragSlot = ActionUI.slotList[i];
                return true;
            }
        }
        return false;
    }
    public bool checkInEquipment(Vector3 mousePos)
    {
        for (int i = 0; i < EquipmentUI.slotList.Length; ++i)
        {
            RectTransform t = (RectTransform)EquipmentUI.slotList[i].transform;

            if (RectTransformUtility.RectangleContainsScreenPoint(t, mousePos))
            {
                DragSlot = EquipmentUI.slotList[i];
                return true;
            }
        }
        return false;
    }

    public void checkQuestItemInBag(string questItemName)//接受任务时检查任务物品是否存在于背包中
    {
        foreach (var item in InventoryData.itemList)
        {
            if (item.itemData != null)
            {
                if (item.itemData.itemName == questItemName)
                    QuestManager.Instance.UpdateQuestProcess(item.itemData.itemName,item.amount);
            }
        }
        foreach (var item in ActionData.itemList)
        {
            if (item.itemData != null)
            {
                if (item.itemData.itemName == questItemName)
                    QuestManager.Instance.UpdateQuestProcess(item.itemData.itemName, item.amount);
            }
        }
    }

    public InventoryItem getQusetItemInBag(ItemData_SO questItem)//检查存在与包中的任务所需物品
    {
        return InventoryData.itemList.Find(i => i.itemData == questItem);
    }
    public InventoryItem getQusetItemInAction(ItemData_SO questItem)//检查存在与快捷栏中的任务所需物品
    {
        return ActionData.itemList.Find(i => i.itemData == questItem);
    }
}
