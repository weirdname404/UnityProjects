using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdHealth : MonoBehaviour {

    public bool alive;

    [SerializeField]
    private GameObject pickupPrefab;

    // Use this for initialization
    void Start () {
        alive = true;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("BigFrog") && alive == true) {
            this.alive = false;

            // Create pickup particles
            Instantiate(pickupPrefab, transform.position, Quaternion.identity);
        }
    }
}
