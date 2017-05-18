using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreCounter : MonoBehaviour {

    public static int score;
    public Text scoreCounterText;

	// Use this for initialization
	void Start () {
        score = 0;
        scoreCounterText = GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {
        scoreCounterText.text = score + "/" + GameState.fliesToWin + " Flies";
	}
}
