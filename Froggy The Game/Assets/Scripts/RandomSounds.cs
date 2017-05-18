using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSounds : MonoBehaviour {

    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> soundClips = new List<AudioClip>();
    [SerializeField]
    private float soundTimerDelay = 3f;
    private float soundTimer;


	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update() {
        // Increment a timer to count up to restarting 
        soundTimer += Time.deltaTime;

        // If the timer reaches the delay ...
        if (soundTimer >= soundTimerDelay) {

            // reset the timer 
            soundTimer = 0f;

            // choose a random sound 
            AudioClip randomSound = soundClips[Random.Range(0, soundClips.Count - 1)];

            // and play the sound
            audioSource.PlayOneShot(randomSound);
        } 
	}
}