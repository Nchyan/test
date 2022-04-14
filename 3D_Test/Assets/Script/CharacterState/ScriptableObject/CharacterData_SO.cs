using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data",menuName = "characterData/Data")]
public class CharacterData_SO : ScriptableObject
{
    [Header("StateInfo")]
    public int maxHp;//生命上限
    public int Hp;//生命值
    public int Ac;//护甲值
}
