using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : EnemyBase {

    [SerializeField]
    private float m_range = 5.0f;

    [SerializeField]
    private float m_firingCone = 5.0f;

    private Transform m_target;

    [SerializeField]
    private float m_shotsPerSecond = 1.0f;
    private float m_shotCoolDown;

    private EnemyBulletPooler m_enemyBulletPooler;

    [SerializeField]
    private float m_enemyBulletSpeed;

    void Start()
    {
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
        m_enemyBulletPooler = GameObject.FindObjectOfType<EnemyBulletPooler>();
        m_shotCoolDown = m_shotsPerSecond;
    }

    // Update is called once per frame
    void Update () {
        if (m_shotCoolDown > 0f) m_shotCoolDown -= Time.deltaTime;

        float distanceToTarget = Vector3.Distance(transform.position, m_target.position);
        if (distanceToTarget <= m_range)
        {
            TurnTorwardsTarget(m_target.position);
            
            // if angle is within fire threshold
            float angleToTarget = Vector2.SignedAngle(transform.up, m_target.position - transform.position);
            if (Mathf.Abs(angleToTarget) < m_firingCone)
            {
                if (m_shotCoolDown <= 0f)
                {
                    Shoot();
                    m_shotCoolDown = m_shotsPerSecond;
                    Debug.Log(m_shotsPerSecond);
                }

            }

        }
		
	}

    private void Shoot()
    {
        GameObject newBullet = m_enemyBulletPooler.GetEnemyBullet();
        newBullet.transform.position = transform.TransformPoint(new Vector3(0f,0.75f,0f));
        newBullet.transform.rotation = transform.rotation;
        newBullet.GetComponent<Rigidbody2D>().velocity = newBullet.transform.up * m_enemyBulletSpeed;
    }
}
