using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public CharacterData_SO enemyBaseData;//状态模板
    public CharacterData_SO enemyData;//个体实际状态数据

    public AttackData_SO atkBaseData;
    public AttackData_SO atkData;

    public float atkCD;//攻击冷却

    private void Awake()
    {
        if (enemyBaseData != null)
            enemyData = Instantiate(enemyBaseData);
        if (atkBaseData != null)
            atkData = Instantiate(atkBaseData);
    }

    #region baseData
    public int maxHp
    {
        get { if (enemyData != null) return enemyData.maxHp; else return 0; }
        set { enemyData.maxHp = value; }
    }
    public int Hp
    {
        get { if (enemyData != null) return enemyData.Hp; else return 0; }
        set { enemyData.Hp = value; }
    }
    public int Ac
    {
        get { if (enemyData != null) return enemyData.Ac; else return 0; }
        set { enemyData.Ac = value; }
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

    #region Character Combat
    public void takeDamage(PlayerState player)//对玩家虚出伤害
    {
        int damage = Mathf.Max(CurDamage() - player.Ac, 1);//计算护甲
        if (damage >= (player.Hp)/3)//打出硬直
        {
            player.GetComponent<Animator>().SetTrigger("Hit");
        }
        player.Hp = Mathf.Max(player.Hp - damage, 0);
        GameManager.Instance.playerStats.rageCon.damageRage(damage);
    }
    public int CurDamage()//单次实际伤害
    {
        float damage = Random.Range(minDamage, maxDamage);
        return (int)damage;
    }
    #endregion
}
