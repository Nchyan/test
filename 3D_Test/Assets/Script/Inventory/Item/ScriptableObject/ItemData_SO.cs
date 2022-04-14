using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum itemType { Useable, Weapon, Action, AC}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData_SO : ScriptableObject
{
    public itemType type;
    public string itemName;//物品名
    public int amount;//数量
    public Sprite itemImg;//图片
    public bool stackable;//可否堆叠
    [TextArea]
    public string detail;//介绍

    [Header("Useable")]
    public GameObject apply;

    [Header("Weapon")]
    public GameObject i;
}
