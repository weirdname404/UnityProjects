using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpawner : MonoBehaviour {

    [SerializeField]
    private GameObject flyPrefab;

    [SerializeField]
    private int totalFlyMinimum = 12;

    private float spawnArea = 25f;
    public static int totalFlies;
    

	// Use this for initialization
	void Start () {
        totalFlies = 0;
    }
	
	// Update is called once per frame
	void Update () {
        while (totalFlies < totalFlyMinimum) {
            totalFlies++;

            float posX = Random.Range(-spawnArea, spawnArea);
            float posZ = Random.Range(-spawnArea, spawnArea);
            Vector3 newPosition = new Vector3(posX, 2f, posZ);

            // create WITHOUT rotation
            Instantiate(flyPrefab, newPosition, Quaternion.identity);
        }
    }
}
