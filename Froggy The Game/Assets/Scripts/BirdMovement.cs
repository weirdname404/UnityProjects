using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BirdMovement: MonoBehaviour {

    private bool isHunting;
    private bool isWalking;
    private bool isChecked;

    [SerializeField]
    private Transform target;
    private NavMeshAgent birdAgent;
    private Animator birdAnimator;
    [SerializeField]
    private RandomSounds birdFootsteps;

    // Use this for initialization
    void Start () {
        birdAgent = GetComponent<NavMeshAgent>();
        birdAnimator = GetComponent<Animator>();

        // Bird walks, then runs
        isWalking = true;

        // Bird is hunting, then retreating
        isHunting = true;

        // We want to check player's power just once, not every frame
        isChecked = false;

        // Tag updated
        this.tag = "Enemy";
    }
	
	// Update is called once per frame
	void Update () {

        if (ScoreCounter.score == GameState.fliesToSprint && isWalking) {
            // Increase the speed and acceleration of the bird
            birdAgent.acceleration = 15;
            birdAgent.speed = 16;
            isWalking = false;
        }

        if (ScoreCounter.score == GameState.fliesToWin && !isChecked) {
            // Retreat!
            isHunting = false;
            this.tag = "Victim";
            isChecked = true;
        }

        if (isHunting) {
            // Set the bird's destination 
            birdAgent.SetDestination(target.position);
        } else {
            // Enemy retreats
            birdAgent.SetDestination(transform.position - target.position);
        }

        // Mesure the magnitude of the NavMeshAgent's velocity
        float speed = birdAgent.velocity.magnitude;

        // Pass the velocity to the Animator's velocity
        birdAnimator.SetFloat("Speed", speed);

        if (speed > 0f) {
            birdFootsteps.enabled = true;
        } else {
            birdFootsteps.enabled = false;
        }
	}
}
