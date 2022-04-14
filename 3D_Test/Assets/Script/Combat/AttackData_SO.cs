using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "AttackData/Data")]
public class AttackData_SO : ScriptableObject
{
    [Header("Attack")]
    public int maxDamage;//��������˺�
    public int minDamage;//������С�˺�
    public int atkRange;//��������
    public float atkSpeed;//�����ٶ�
}
