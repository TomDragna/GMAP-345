using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class empty_position : MonoBehaviour {

    public GameObject player;

    private float minDistance;
    private Vector3 offset;

    void Start ()
    {
        minDistance = Vector3.Distance(this.transform.position, player.transform.position);

        offset = transform.position - player.transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Player_controller s_controller = player.GetComponent<Player_controller>();
        float scaleRate = s_controller.scaleRate;

        transform.localScale += new Vector3(scaleRate, scaleRate, scaleRate);

        this.transform.position = player.transform.position + offset;
        //this.transform.localPosition = new Vector3(0, 0, 0);



    }
}
