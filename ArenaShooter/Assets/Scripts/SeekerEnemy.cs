using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerEnemy : EnemyBase {

    private Transform m_player;
    private bool m_playerFound = false;
    private Vector3 m_target = Vector3.zero;
    [SerializeField]
    private float m_positionReachedThreshold = .5f;
    [SerializeField]
    private LayerMask m_searchLineCollidesWith;

	// Use this for initialization
	void Start () {
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        FindNextPosition();
	}
	
	// Update is called once per frame
	void Update () {
		if(Physics2D.Linecast(transform.position, m_player.position, m_searchLineCollidesWith).transform == m_player)
        {
            //found player, move directly to him
            if (!m_playerFound)
            {
                //Debug.Log("Player found");
                m_playerFound = true;
            }

            HandleMovement(m_player.position);
        } else
        {
            if (m_playerFound)
            {
                //Debug.Log("Player lost");
                m_playerFound = false;
                FindNextPosition();
            }

            if (Vector3.Distance(transform.position, m_target) < m_positionReachedThreshold)
                FindNextPosition();

            HandleMovement(m_target);
        }
	}

    private void FindNextPosition()
    {
        //Debug.Log("Getting next position");
        m_target = Pathfinder.Instance.GetNextPosition(m_player.position, transform.position);
        //Debug.Log("Next position is " + m_target);
    }

    private void OnDrawGizmos()
    {
        if (m_playerFound)
        {
            Gizmos.DrawLine(m_player.transform.position, transform.position);
        } else 
            Gizmos.DrawLine(m_target, transform.position);
    }
}
