using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointPickupScript : MonoBehaviour {

    [SerializeField]
    private int m_hitpoints;

    public int Hitpoints { get { return m_hitpoints; } }
}
