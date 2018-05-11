using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPooler : MonoBehaviour {

    [SerializeField]
    private GameObject m_playerBulletPrefab;
    private List<GameObject> m_playerBulletPool = new List<GameObject>();

	public GameObject GetPlayerBullet() {
        if (m_playerBulletPool.Count > 0) {
            //Store bullet for a moment
            GameObject bulletToReturn = m_playerBulletPool[0];

            //Remove bullet from pool
            m_playerBulletPool.RemoveAt(0);

            //Re-enable bullet
            bulletToReturn.SetActive(true);

            //Return bullet
            return bulletToReturn;
        }
        else {
            // need to add in a reference to this pooler when we generate the 
            // bullet so that it will know where to return to when destroyed
            GameObject newBullet = Instantiate(m_playerBulletPrefab);
            newBullet.GetComponent<PlayerBulletScript>().Initialize(this);
            return newBullet;
        }
    }

    public void ReturnPlayerBulletToPool(GameObject bullet) {
        //Disable bullet
        bullet.SetActive(false);

        //Add to pool
        m_playerBulletPool.Add(bullet);
    }

}
