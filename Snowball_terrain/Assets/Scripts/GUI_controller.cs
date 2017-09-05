using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI_controller : MonoBehaviour {

    public GameObject player;
    public Text speedText;
    public Text pointText;
    public Text sizeText;
    public Text gameState; //win or lose, etc.
    public GameObject restartButton;

    private float speed;
    private float diameter;
    private double size;
    private double mph;
    private int points;
    private int score;
    // Use this for initialization
    void Start ()
    {
        
        speed = 0.0f;
        diameter = 0.0f;
        points = 0;

        UpdateMph();
        UpdateSize();
        UpdateScore();
    }

    private void LateUpdate()
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        Renderer rend = player.GetComponent<Renderer>();

        bool gameLost = player.GetComponent<Player_controller>().gameLost;
        bool gameWon = player.GetComponent<Player_controller>().gameWon;


        if (!gameLost && !gameWon) {
            diameter = rend.bounds.size.x * 3.28f;
            speed = rb.velocity.magnitude * 2.2369362912f;
            points = 5;
            //Debug.Log(points);
            AddSize(diameter);
            AddMph(speed);
            AddPoints(points);
        } else if (gameLost) {
            gameState.text = "You Lose";
            restartButton.SetActive(true);
        } else if (gameWon) {
            gameState.text = "You Win" + '\n' + "Your Score is: " + score;
            restartButton.SetActive(true);
        } else {
			//speedText.text = "OVER";
		}


        if (Input.GetKey("escape")) Application.Quit();
    }

    void AddSize(float fat)
    {

        size = Mathf.Round(fat * 100f) / 100f;
        UpdateSize();
    }
    void AddMph(float newMph)
    {

        mph = Mathf.Round(newMph * 100f) / 100f;
        UpdateMph();
    }
    public void AddPoints(int points)
    {

        score += points;
        UpdateScore();
    }

	public void subtractPoints(int points){
		score -= points;
		UpdateScore ();
	}

    void UpdateSize()
    {
        sizeText.text = "SIZE: " + size + " Ft";
    }
    void UpdateMph()
    {
        speedText.text = "SPEED: " + mph + " MPH";
    }
    void UpdateScore()
    {
        pointText.text = "Points: " + score;
    }

    public void restartGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
