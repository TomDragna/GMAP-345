using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class GUI_controller : MonoBehaviour {

    public GameObject player;
    public Text speedText;
    public Text pointText;

    private float speed;
    private double mph;
    private int points;
    private int score;
    // Use this for initialization
    void Start ()
    {
        
        speed = 0.0f;
        points = 0;
        UpdateMph();
        UpdateScore();
    }

    private void LateUpdate()
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        speed = rb.velocity.magnitude;
        points = 10;
        Debug.Log(points);
        AddMph(speed);
        AddPoints(points);
    }

    void AddMph(float newMph)
    {

        mph = Mathf.Round(newMph * 100f) / 100f; ;
        UpdateMph();
    }
    void AddPoints(int points)
    {

        score += points;
        UpdateScore();
    }

    void UpdateMph()
    {
        speedText.text = mph + " MPH";
    }
    void UpdateScore()
    {
        pointText.text = "Points: " + score;
    }
}
