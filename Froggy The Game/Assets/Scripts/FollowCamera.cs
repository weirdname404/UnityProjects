using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    private bool a = true;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Vector3 offset;
    private float cameraFollowSpeed = 5f;

	
	// Update is called once per frame
	void Update () {
        Vector3 newPosition = player.position + offset;

        transform.position = Vector3.Lerp(transform.position, newPosition, cameraFollowSpeed * Time.deltaTime);

        if (ScoreCounter.score >= GameState.fliesToSprint && a) {
            offset += new Vector3(0f, 1.5f, 0f);
            transform.eulerAngles += new Vector3(10f, 0f, 0f);

            a = false;
        }
	}
}
