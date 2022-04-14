using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "AttackData/Data")]
public class AttackData_SO : ScriptableObject
{
    [Header("Attack")]
    public int maxDamage;//ÎäÆ÷×î´óÉËº¦
    public int minDamage;//ÎäÆ÷×îĞ¡ÉËº¦
    public int atkRange;//¹¥»÷¾àÀë
    public float atkSpeed;//¹¥»÷ËÙ¶È
}
