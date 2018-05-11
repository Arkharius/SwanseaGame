using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPooler : MonoBehaviour {

    [SerializeField]
    private GameObject m_enemyBulletPrefab;
    private List<GameObject> m_enemyBulletPool = new List<GameObject>();

    public GameObject GetEnemyBullet()
    {
        if (m_enemyBulletPool.Count > 0)
        {
            //Store bullet for a moment
            GameObject bulletToReturn = m_enemyBulletPool[0];

            //Remove bullet from pool
            m_enemyBulletPool.RemoveAt(0);

            //Re-enable bullet
            bulletToReturn.SetActive(true);

            //Return bullet
            return bulletToReturn;
        }
        else
        {
            // need to add in a reference to this pooler when we generate the 
            // bullet so that it will know where to return to when destroyed
            GameObject newBullet = Instantiate(m_enemyBulletPrefab);
            newBullet.GetComponent<EnemyBulletScript>().Initialize(this);
            return newBullet;
        }
    }

    public void ReturnEnemyBulletToPool(GameObject bullet)
    {
        //Disable bullet
        bullet.SetActive(false);

        //Add to pool
        m_enemyBulletPool.Add(bullet);
    }

}
