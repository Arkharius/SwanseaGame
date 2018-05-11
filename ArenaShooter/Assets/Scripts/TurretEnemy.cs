using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : EnemyBase {

    [SerializeField]
    private float m_range = 5.0f;

    [SerializeField]
    private float m_firingCone = 5.0f;

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
            float angleToTarget = Vector2.SignedAngle(transform.up, m_target.position - transform.position);
            if (Mathf.Abs(angleToTarget) < m_firingCone)
            {
                Debug.Log(angleToTarget);
                // Shoot();
            }

        }
		
	}
}
