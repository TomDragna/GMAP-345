﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour {

    public float speed;
	public float growEvery = 10f;	//how frequently the over-time growth is done
	public float scaleRate;		//size gained over time
	public float scaleRateBonus;	//size gained from skier collisions
	public float scaleCap;		//max size allowed
	public float descaleRate;	//growth penalty for obstacle collisions
    public float deaccellX;

    private float weirdo;
    private float timeToGrow;
	private Vector3 startScale;	//used for lose condition i.e: if current scale < start, you're dunzo
	public bool gameLost;
	public bool gameWon;


    private Rigidbody rb;
	// Use this for initialization
	void Start ()
    {
		gameLost = false;
		gameWon = false;
        rb = GetComponent<Rigidbody>();	
		timeToGrow = growEvery;
		//growFlag = false;
		startScale = transform.localScale;
	}

	void FixedUpdate ()
    {
        Renderer rend = this.GetComponent<Renderer>();

        if (transform.localScale.x < startScale.x && transform.localScale.y < startScale.y && transform.localScale.z < startScale.z) {
			//lose condition check
			Debug.Log ("you lost");
			gameLost = true;
			gameOver ();
		}
		timeToGrow -= Time.deltaTime;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //controls "8" = deaccelerator
        Vector3 movement = new Vector3(-moveVertical / deaccellX, 0.0f, moveHorizontal * 2);

        

		if (timeToGrow <= 0f) {
			if(transform.localScale.x < scaleCap && transform.localScale.y < scaleCap && transform.localScale.z < scaleCap){
                //Debug.Log ("Growing");
                transform.localScale += new Vector3 (scaleRate,scaleRate,scaleRate);

                if (rb.velocity.magnitude <= 89.5)
                {
                    rb.AddForce(movement * speed);
                }

                if (((rend.bounds.size.x * 3.28f) >= 7))
                {
                    weirdo = speed * 2.5f;
                    if (rb.velocity.magnitude <= 89.5)
                    {
                        rb.AddForce(movement * weirdo);
                    }
                }
            }
			timeToGrow = growEvery;
		}
    }


	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Skier") {
			Debug.Log ("Good collision");
			transform.localScale += new Vector3 (scaleRateBonus, scaleRateBonus, scaleRateBonus);
			//score stuff
		}else if(other.gameObject.tag == "Obstacle"){
			Debug.Log ("bad collision");
			transform.localScale -= new Vector3 (descaleRate,descaleRate,descaleRate);
			//score stuff
		}

	}

	void gameOver(){
		rb.constraints = RigidbodyConstraints.FreezeAll;

	}
}
