using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data",menuName = "characterData/Data")]
public class CharacterData_SO : ScriptableObject
{
    [Header("StateInfo")]
    public int maxHp;//��������
    public int Hp;//����ֵ
    public int Ac;//����ֵ
}
