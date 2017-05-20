using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement: MonoBehaviour {

    private Animator playerAnimator;
    private Vector3 movement;
    private Rigidbody playerRigidBody;
    private float moveHorizontal;
    private float moveVertical;
    private float turnSpeed = 20f;
    private bool isNotTriggered;

    [SerializeField]
    private RandomSounds playerFootsteps;


    // Use this for initialization
    void Start () {
        // Gather components from the Player GameObject
        playerAnimator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();

        // Super Power disabled
        isNotTriggered = true;

        // Normal scale update
        transform.localScale = new Vector3(1f, 1f, 1f);
        transform.position = new Vector3(0f, 1.225f, 0f);

        // Tag updated
        this.tag = "Player";

    }
	
	// Update is called once per frame
	void Update () {

        // Android
        // NOTE: If you want to enable control movement via joystick on Android platform uncomment the line below
        movement = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0.0f, CrossPlatformInputManager.GetAxis("Vertical"));

        // PC
        // NOTE: Lines below enable control movement via WASD on PC platforms
        // NOTE: If you want to change platform to mobile, do not forget to comment lines below
        // NOTE: Do not forget to uncomment line in GameState.cs and change scene in Unity

        // ### COMMENT THESE LINES TO CHANGE THE PLATFORM TO MOBILE  

        //// Gather input -1 or 1, to guess in what direction the frog will move
        //moveHorizontal = Input.GetAxisRaw("Horizontal");
        //moveVertical = Input.GetAxisRaw("Vertical");
        //// Combining X and Z
        //movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // ###################################################################

        // Activate Super Power
        if (ScoreCounter.score == GameState.fliesToWin && isNotTriggered) {
            activatePlayerPower();
            isNotTriggered = false;
        }
	}

    // Calculation of physics here
    void FixedUpdate() { 

       if (movement != Vector3.zero) {
            // Create a target rotation based on the movement vector
            Quaternion targetRoataion = Quaternion.LookRotation(movement, Vector3.up);

            // and create another rotation that moves from the current rotation to the target rotation 
            Quaternion newRotation = Quaternion.Lerp(playerRigidBody.rotation, targetRoataion, turnSpeed * Time.deltaTime);

            // and change the player's rotation to the new incremental rotation
            playerRigidBody.MoveRotation(newRotation);

            // play jump animation
            playerAnimator.SetFloat("Speed", 3f);

            // ... play footsteps sounds
            playerFootsteps.enabled = true;

        } 
       
       else {
            playerAnimator.SetFloat("Speed", 0f);
            playerFootsteps.enabled = false;
        }
    }

    private void activatePlayerPower() {
        // Increase the scale of the frog
        transform.localScale += new Vector3(0.7f, 0.7f, 0.7f);
        transform.position += new Vector3(0f, 0.2f, 0f);

        this.tag = "BigFrog";
    }
}
