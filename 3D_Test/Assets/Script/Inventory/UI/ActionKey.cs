using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionKey : MonoBehaviour
{
    public KeyCode actionKey;
    private ItemSlot curItem;
    void Start()
    {
        curItem = GetComponent<ItemSlot>();
    }

    void Update()
    {
        if (Input.GetKeyDown(actionKey) && curItem.itemUI.getItem() != null)
            curItem.useItem();
    }
}
