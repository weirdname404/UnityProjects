using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class GameState : MonoBehaviour {

    private bool gameStarted = false;

    public static int fliesToWin = 50;
    public static int fliesToSprint = 20;

    [SerializeField]
    private Text gameStateText;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject bird;
    [SerializeField]
    private BirdMovement birdMovement;
    [SerializeField]
    private FollowCamera followCamera;

    private float restartDelay = 3f;
    private float restartTimer;
    private float textTimer;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private BirdHealth birdHealth;

    // For 1st notification
    private bool wasNotShown;
    private bool firstTime;

    // For text repeat
    private bool transmit;
    private bool repeat;


    // Use this for initialization
    void Start () {
        // Cursor.visible = false;

        playerMovement = player.GetComponent<PlayerMovement>();
        playerHealth = player.GetComponent<PlayerHealth>();
        birdHealth = bird.GetComponent<BirdHealth>();

        // Prevent the player from moving at the start of the game
        playerMovement.enabled = false;
        // Prevent the bird from moving 
        birdMovement.enabled = false;
        // Prevent the follow camera from moving to its game position
        followCamera.enabled = false;

        wasNotShown = true;
        firstTime = true;

        transmit = true;
        repeat = false;
    }
	
	// Update is called once per frame
	void Update () {
        
        // press ESC to quit
        if (gameStarted == false && Input.GetKeyUp(KeyCode.Escape)) {
            Application.Quit();
        }

        // Informing player about chasing bird
        if (ScoreCounter.score >= fliesToSprint && wasNotShown) {
            if (firstTime) {
                startTextTransmisson("Beware bird is chasing you!");
                firstTime = false;
            }

            textTimer += Time.deltaTime;

            if (textTimer >= 3f) {
                endTextTransmission();
                wasNotShown = false;
                textTimer = 0f;
            }

        } 
        
        // Informing that player can eat the bird
        else if (ScoreCounter.score >= fliesToWin) {


            if (transmit) {
                if (textTimer <= 0) {
                    startTextTransmisson("EAT the BIRD!");
                }

                textTimer += Time.deltaTime;

                if (textTimer >= 2f) {
                    transmit = false;
                    repeat = true;
                }
            }

            if (repeat) {
                if (textTimer >= 2f) {
                    endTextTransmission();
                }

                textTimer -= Time.deltaTime;

                if (textTimer <= 0f) {
                    transmit = true;
                    repeat = false;
                }
            }
        }

        // if the game is not started and the player presses the spacebar

        // NOTE: uncomment the line below in order to make game start on Android platform
        if (gameStarted == false && CrossPlatformInputManager.GetButtonUp("Start")) 
        {
            //...start the game
            StartGame();
        }

        // NOTE: PC version game state control
        // NOTE: uncomment the line below in order to make game start on PC platform
        //if (gameStarted == false && Input.GetKeyUp(KeyCode.Space))
        //{ 
        //    //...start the game
        //    StartGame();
        //}

        // if the player is no longer alive 
        else if (playerHealth.alive == false || birdHealth.alive == false) {

            // ...end the game ..
            EndGame();

            // ...increment a timer to count up to restarting..
            restartTimer += Time.deltaTime;
            // ...and if it reaches the restartDelay 
            if (restartTimer >= restartDelay) {
                // ...then reload the currently loaded scene 
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
	}

    private void StartGame() {

        // Set the game state
        gameStarted = true;

        // Remove the start text
        gameStateText.color = Color.clear;

        // Allow the bird, camera and player to move
        playerMovement.enabled = true;
        birdMovement.enabled = true;
        followCamera.enabled = true;
    }

    private void EndGame() {

        // Set the game state
        gameStarted = false;

        if (playerHealth.alive == true) {
            gameStateText.color = Color.white;
            gameStateText.text = "You Won!";
            // Remove the bird from the game
            bird.SetActive(false);

        } else {
            // Show the game over text
            gameStateText.color = Color.white;
            gameStateText.text = "Game Over!";

            // Remove the player from the game
            player.SetActive(false);
        }
        
    }

    private void startTextTransmisson(string text) {

        // Set Text
        gameStateText.color = Color.red;
        gameStateText.fontSize = 60;
        gameStateText.text = text;

    }

    private void endTextTransmission() {

        // Clear Text
        gameStateText.color = Color.clear;

    }
}
