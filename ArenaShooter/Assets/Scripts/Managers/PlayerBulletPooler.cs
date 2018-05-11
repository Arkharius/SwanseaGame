using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPooler : MonoBehaviour {

    [SerializeField]
    private GameObject m_playerBulletPrefab, m_playerPowerShotPrefab;
    private List<GameObject> m_playerBulletPool = new List<GameObject>();
    private List<GameObject> m_playerPowerShotPool = new List<GameObject>();

    public GameObject GetPowerShot() {
        if (m_playerPowerShotPool.Count > 0) {
            GameObject powerShotToReturn = m_playerPowerShotPool[0];
            m_playerPowerShotPool.RemoveAt(0);
            powerShotToReturn.SetActive(true);

            return powerShotToReturn;
        }
        else {
            GameObject newPS = Instantiate(m_playerPowerShotPrefab);
            newPS.GetComponent<PlayerPowerShotScript>().Initialize(this);
            return newPS;
        }
    }

    public void ReturnPlayerPowerShotToPool(GameObject bullet) {
        //Disable bullet
        bullet.SetActive(false);

        //Add to pool
        m_playerPowerShotPool.Add(bullet);
    }

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
