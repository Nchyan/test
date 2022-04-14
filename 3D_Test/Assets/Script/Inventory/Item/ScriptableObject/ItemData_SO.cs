using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum itemType { Useable, Weapon, Action, AC}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData_SO : ScriptableObject
{
    public itemType type;
    public string itemName;//��Ʒ��
    public int amount;//����
    public Sprite itemImg;//ͼƬ
    public bool stackable;//�ɷ�ѵ�
    [TextArea]
    public string detail;//����

    [Header("Useable")]
    public GameObject apply;

    [Header("Weapon")]
    public GameObject i;
}
