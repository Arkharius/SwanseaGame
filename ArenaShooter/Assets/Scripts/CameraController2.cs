using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private GameObject m_player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // get player's facing direction + ahead by amount
        // get player's velocity
        // smooth camera to average of both

        // Basic system
        transform.position = new Vector3(m_player.transform.position.x, m_player.transform.position.y, transform.position.z);
	}
}
