using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Base Setting")]
    public float force;
    public GameObject target;//飞向的目标
    public Vector3 dir;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        flyToTarget();
    }
    public void flyToTarget()
    {
        if (target == null)
            target = FindObjectOfType<PlayerController>().gameObject;

        dir = (target.transform.position - transform.position + Vector3.up).normalized;
        rb.AddForce(dir * force , ForceMode.Impulse);
    }

}
