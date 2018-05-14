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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //with the collision layers, we know what we're going to collide with, so we just see if it's damageable and apply damage if we can.
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
            damageable.TakeDamage(m_damage);

        // destroy bullet
        Die();
    }
    // return the bullet to the pooler
    private void Die()
    {
        Instantiate(m_impactEffect, transform.position, Quaternion.identity);
        m_pooler.ReturnEnemyBulletToPool(gameObject);
    }
}