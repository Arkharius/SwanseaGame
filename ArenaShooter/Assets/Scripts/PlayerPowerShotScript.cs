﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerShotScript : MonoBehaviour {

    [SerializeField]
    private int m_damage;

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
        m_pooler.ReturnPlayerPowerShotToPool(gameObject);
    }
}