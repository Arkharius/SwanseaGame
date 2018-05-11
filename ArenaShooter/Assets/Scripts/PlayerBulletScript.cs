using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour {

    [SerializeField]
    private int m_damage;

    [SerializeField]
    private GameObject m_impactEffect;

    private PlayerBulletPooler m_pooler;

    public void Initialize(PlayerBulletPooler pooler) {
        m_pooler = pooler;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
            // set collision enemy to take damage
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(m_damage);
            // destroy bullet
            Die();
        }
    }
    // return the bullet to the pooler
    private void Die() {
        Instantiate(m_impactEffect, transform.position, Quaternion.identity);
        m_pooler.ReturnPlayerBulletToPool(gameObject);
    }
}