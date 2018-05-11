using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : EnemyBase {

    [SerializeField]
    private float m_range = 2.0f;

    private Transform m_target;

    void Start()
    {
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update () {
        float distanceToTarget = Vector3.Distance(transform.position, m_target.position);
        if (distanceToTarget <= m_range)
        {
            TurnTorwardsTarget(m_target.position);

            // if angle is within fire threshold
            // Shoot();
        }
		
	}
}
