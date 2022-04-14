using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public CharacterData_SO playerBaseData;
    public CharacterData_SO playerData;
    public AttackData_SO atkBaseData;
    public AttackData_SO atkData;
    public RageData_SO rageBaseData;
    public RageData_SO rageData;

    public RageController rageCon;//怒气控制器

    private void Awake()
    {
        rageCon = GetComponent<RageController>();

        if (playerBaseData != null)
            playerData = Instantiate(playerBaseData);
        if (atkBaseData != null)
            atkData = Instantiate(atkBaseData);
        if (rageBaseData != null) 
        {
            rageData = Instantiate(rageBaseData);
            rageCon.playerRage = rageData;
        }
            
    }
    //详细属性
    #region baseData
    public int maxHp 
    { 
        get { if (playerData != null) return playerData.maxHp; else return 0; }
        set { playerData.maxHp = value; }
    }
    public int Hp
    {
        get { if (playerData != null) return playerData.Hp; else return 0; }
        set { playerData.Hp = value; }
    }
    public int Ac
    {
        get { if (playerData != null) return playerData.Ac; else return 0; }
        set { playerData.Ac = value; }
    }
    #endregion
    #region atkData
    public int maxDamage
    {
        get { if (atkData != null) return atkData.maxDamage; else return 0; }
        set { atkData.maxDamage = value; }
    }
    public int minDamage
    {
        get { if (atkData != null) return atkData.minDamage; else return 0; }
        set { atkData.minDamage = value; }
    }
    public int atkRange
    {
        get { if (atkData != null) return atkData.atkRange; else return 0; }
        set { atkData.atkRange = value; }
    }
    public float atkSpeed
    {
        get { if (atkData != null) return atkData.atkSpeed; else return 0; }
        set { atkData.atkSpeed = value; }
    }
    #endregion
    #region rageData
    public int maxRage
    {
        get { if (rageData != null) return rageData.maxRage; else return 0; }
        set { rageData.maxRage = value; }
    }
    public int Rage
    {
        get { if (rageData != null) return rageData.Rage; else return 0; }
        set { rageData.Rage = value; }
    }
    #endregion
    #region Character Combat
    public void takeDamage(EnemyState enemy)//对敌人虚出伤害
    {
        int damage = Mathf.Max(CurDamage() - enemy.Ac, 1);//计算护甲
        enemy.Hp = Mathf.Max(enemy.Hp - damage, 0);
        rageCon.hitRage(damage);
    }

    public int CurDamage()//单次实际伤害
    {
        float damage = Random.Range(minDamage, maxDamage);
        return (int)damage;
    }
    #endregion
    #region Health change
    public void applyHealth(int value)//非物理途径的改变血量
    {
        if (Hp + value <= maxHp)
            Hp += value;
        else//回血量溢出
            Hp = maxHp;
    }
    #endregion

}
