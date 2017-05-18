using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyParticleDestruction : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // Destroy the pickupParticles after 5 seconds
        Destroy(gameObject, 5f);
	}

}
