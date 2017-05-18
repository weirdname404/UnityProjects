using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPickup : MonoBehaviour {

    [SerializeField]
    private GameObject pickupPrefab;

    void OnTriggerEnter(Collider other) {

        if (other.tag == "Player" || other.tag == "BigFrog") {

            // add the pickup particles (no rotation)
            Instantiate(pickupPrefab, transform.position, Quaternion.identity);

            FlySpawner.totalFlies--;
            ScoreCounter.score++;
 
            Destroy(gameObject);
        }
    }
}
