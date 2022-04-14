using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerUI : MonoBehaviour
{
    public ItemSlot[] slotList;

    public void printUI()
    {
        for (int i = 0; i < slotList.Length; ++i)
        {
            slotList[i].itemUI.Index = i;
            slotList[i].printItem();
        }
    }
}
