using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [System.Serializable]
    public class LootItem
    {
        public GameObject item;
        [Range(0, 1)]
        public float weight;
    }

    public LootItem[] Lootlist;

    public void spawnLoot()
    {
        float curRandom = Random.value;

        for (int i = 0; i < Lootlist.Length; ++i)
        {
            if (curRandom <= Lootlist[i].weight)//±¬³öÎïÆ·
            {
                GameObject obj = Instantiate(Lootlist[i].item);
                obj.transform.position = transform.position + Vector3.up * 3;
            }
        }
    }
}
