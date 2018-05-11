using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour {

    [SerializeField]
    private int m_damage;

    [SerializeField]
    private GameObject m_impactEffect;

    private EnemyBulletPooler m_pooler;

    public void Initialize(EnemyBulletPooler pooler)
    {
        m_pooler = pooler;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // set collision enemy to take damage
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(m_damage);
            // destroy bullet
            Die();
        }
    }
    // return the bullet to the pooler
    private void Die()
    {
        Instantiate(m_impactEffect, transform.position, Quaternion.identity);
        m_pooler.ReturnEnemyBulletToPool(gameObject);
    }
}