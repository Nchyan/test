using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image icon;
    public Text numTxt;

    public ItemData_SO curItemData;

    public InventoryData_SO Bag { set; get; }
    public int Index { set; get; } = -1;
    
    public void setItemUI(ItemData_SO item,int itemNum)
    {
        if (itemNum == 0)//物品个数为0的情况
        {
            Bag.itemList[Index].itemData = null;
            icon.gameObject.SetActive(false);
            return;
        }

        if (itemNum < 0)
            item = null;

        if (item != null)//将数据传递到画面对应格子上
        {
            curItemData = item;
            icon.sprite = item.itemImg;
            numTxt.text = itemNum.ToString();
            icon.gameObject.SetActive(true);
        }
        else
        {
            icon.gameObject.SetActive(false);
        }
    }

    public ItemData_SO getItem()
    {
        return Bag.itemList[Index].itemData;
    }
}
