using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyBase {

    private Transform m_target;

    // Use this for initialization
    public void Start () {
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Update is called once per frame
    public void Update()
    {
        HandleMovement(m_target.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_target == collision.transform)
        {
            m_target.GetComponent<IDamageable>().TakeDamage(5);
            m_rigidbody.velocity = (transform.position - m_target.position) * 5f;
        }
    }
}
