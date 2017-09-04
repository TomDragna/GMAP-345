using System.Collections;
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
	private bool grounded;
	public float jumpMod;
	public GameObject ski_hit;
	public int skiBonus;
	public GameObject snowman_hit;
	public int snowBonus;
	public int obstaclePenalty;
	public GameObject gui;

    private Rigidbody rb;
	// Use this for initialization
	void Start ()
    {
		gameLost = false;
		gameWon = false;
		grounded = true;
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

		if(grounded == false){
			GetComponent<Rigidbody> ().AddForce (Vector3.down * (GetComponent<Rigidbody>().mass*jumpMod));
		}

		timeToGrow -= Time.deltaTime;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //controls "8" = deaccelerator
        Vector3 movement = new Vector3(-moveVertical / deaccellX, 0.0f, moveHorizontal * 2);

        

		if (timeToGrow <= 0f) {
			if(transform.localScale.x < scaleCap && transform.localScale.y < scaleCap && transform.localScale.z < scaleCap && gameLost == false){
                //Debug.Log ("Growing");
                transform.localScale += new Vector3 (scaleRate,scaleRate,scaleRate);

                if (rb.velocity.magnitude <= 111.5)
                {
                    rb.AddForce(movement * speed);
                }

                if (((rend.bounds.size.x * 3.28f) >= 7))
                {
                    weirdo = speed * 2.5f;
                    if (rb.velocity.magnitude <= 111.5)
                    {
                        rb.AddForce(movement * weirdo);
                    }
                    if (rb.velocity.magnitude > 111.5)
                    {
                        rb.AddForce(movement * (weirdo*1.75f));
                        rb.velocity = rb.velocity.normalized * 111.5f;
                    }
                }
            }
			timeToGrow = growEvery;
		}
    }


	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Skier") {
			Debug.Log ("Skier collision");
			transform.localScale += new Vector3 (scaleRateBonus, scaleRateBonus, scaleRateBonus);
			Destroy (other.gameObject);
			GameObject skiHit = Instantiate (ski_hit);
			skiHit.transform.position = transform.position;
			gui.GetComponent<GUI_controller> ().AddPoints (skiBonus);
		}else if(other.gameObject.tag == "Snowman"){
			Debug.Log ("Snowman collision");
			transform.localScale += new Vector3 (scaleRateBonus, scaleRateBonus, scaleRateBonus);
			GameObject snowHit = Instantiate (snowman_hit);
			snowHit.transform.position = transform.position;
			//I want to add something so that the snowmen still get knocked around but get destroyed eventually. still wip for now
			Destroy (other.gameObject);
			gui.GetComponent<GUI_controller> ().AddPoints (snowBonus);
		}else if(other.gameObject.tag == "Obstacle"){
			Debug.Log ("bad collision");
			transform.localScale -= new Vector3 (descaleRate,descaleRate,descaleRate);
			gui.GetComponent<GUI_controller> ().subtractPoints (obstaclePenalty);
		}else if(other.gameObject.tag == "Ground"){
			if (grounded == false) {
				GameObject groundHit = Instantiate (snowman_hit);
				groundHit.transform.position = transform.position;
			}
			grounded = true;
			//Debug.Log ("on ground");
		}

	}

	void OnCollisionExit(Collision other){
		if(other.gameObject.tag == "Ground"){
			grounded = false;
			//Debug.Log ("off ground");
		}
	}

	void gameOver(){
		if(gameLost == true){
			rb.constraints = RigidbodyConstraints.FreezeAll;

			//do x
		}else if(gameWon == true){
			//do y
		}
	}
}
