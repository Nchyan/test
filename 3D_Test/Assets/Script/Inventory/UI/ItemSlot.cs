using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotType{BAG, WEAPON, ACTION, AC }
public class ItemSlot : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public SlotType type;
    public ItemUI itemUI;

    private void OnDisable()
    {
        InventoryManager.Instance.tooltip.gameObject.SetActive(false);//¹Ø±Õtooltip
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount >= 2 && itemUI.getItem() != null)//Ë«»÷Ê¹ÓÃ
            useItem();
    }
    public void useItem()
    {
        if (itemUI.getItem().type == itemType.Useable)
        {
            itemUI.getItem().apply.gameObject.GetComponent<IApplyFunction>().Apply();
            itemUI.Bag.itemList[itemUI.Index].amount -= 1;

            QuestManager.Instance.UpdateQuestProcess(itemUI.getItem().name, -1);
        }
        printItem();
    }
    public void printItem()
    {
        switch (type)
        {
            case SlotType.BAG:
                itemUI.Bag = InventoryManager.Instance.InventoryData;
                break;
            case SlotType.WEAPON:
                itemUI.Bag = InventoryManager.Instance.EquipmentData;
                break;
            case SlotType.ACTION:
                itemUI.Bag = InventoryManager.Instance.ActionData;
                break;
            case SlotType.AC:
                itemUI.Bag = InventoryManager.Instance.EquipmentData;
                break;
        }

        var item = itemUI.Bag.itemList[itemUI.Index];
        itemUI.setItemUI(item.itemData, item.amount);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemUI.getItem() != null)
        {
            InventoryManager.Instance.tooltip.setTooltip(itemUI.getItem());
            InventoryManager.Instance.tooltip.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager.Instance.tooltip.gameObject.SetActive(false);
    }
}
