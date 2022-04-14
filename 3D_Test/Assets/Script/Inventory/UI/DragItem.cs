using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemUI))]
public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    ItemUI curItemUI;
    ItemSlot curItemSlot;
    ItemSlot tarItemSlot;
    private void Awake()
    {
        curItemUI = GetComponent<ItemUI>();
        curItemSlot = GetComponentInParent<ItemSlot>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        InventoryManager.Instance.curDrag = new InventoryManager.DragData();
        InventoryManager.Instance.curDrag.originSlot = transform.gameObject.GetComponentInParent<ItemSlot>();
        InventoryManager.Instance.curDrag.originTrans = (RectTransform)InventoryManager.Instance.curDrag.originSlot.GetComponent<Transform>();
        transform.SetParent(InventoryManager.Instance.DragCanvas.transform,true);//将被拖拽的物体贴到顶层
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (InventoryManager.Instance.checkInInventory(eventData.position) || InventoryManager.Instance.checkInAction(eventData.position) || InventoryManager.Instance.checkInEquipment(eventData.position))
            {
                tarItemSlot = InventoryManager.Instance.DragSlot;//获取目标格子的ItemSlot

                switch (tarItemSlot.type)//判断目标位置的Item种类
                {
                    case SlotType.BAG:
                        swapItem();
                        break;
                    case SlotType.WEAPON:
                        if (curItemUI.getItem().type == itemType.Weapon)
                            swapItem();
                        break;
                    case SlotType.ACTION:
                        if (curItemUI.getItem().type == itemType.Useable)
                            swapItem();
                        break;
                    case SlotType.AC:
                        if (curItemUI.getItem().type == itemType.AC)
                            swapItem();   
                        break;
                }
                curItemSlot.printItem();
                tarItemSlot.printItem();
            }
            
        }
        else //选取物品被扔至世界地图外
        {
        
        }

        transform.SetParent(InventoryManager.Instance.curDrag.originTrans);//将拖拽物体从拖拽层归位

        RectTransform t = (RectTransform)transform;
        t.offsetMax = -Vector2.one * 8;
        t.offsetMin = Vector2.one * 8;
    }

    public void swapItem()
    {
        if (tarItemSlot == curItemSlot)//同格调度物品不变
        {
            return;
        }

        var targetItem = tarItemSlot.itemUI.Bag.itemList[tarItemSlot.itemUI.Index];
        var tempItem = curItemSlot.itemUI.Bag.itemList[curItemSlot.itemUI.Index];

        bool isSame = (targetItem.itemData == tempItem.itemData);//判断两个交换的物体是否相同

        if (isSame && targetItem.itemData.stackable)
        {
            targetItem.amount += tempItem.amount;
            tempItem.itemData = null;
            tempItem.amount = 0;
        }
        else//不可堆叠，换位置
        {
            curItemSlot.itemUI.Bag.itemList[curItemSlot.itemUI.Index] = targetItem;
            tarItemSlot.itemUI.Bag.itemList[tarItemSlot.itemUI.Index] = tempItem;
        }
    }
}
