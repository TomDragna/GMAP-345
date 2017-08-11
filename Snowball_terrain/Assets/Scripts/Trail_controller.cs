using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail_controller : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;

    // Use this for initialization
    void Start ()
    {
        
        offset = transform.position - player.transform.position;
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        
        Player_controller s_controller = player.GetComponent<Player_controller>();
        float scaleRate = s_controller.scaleRate;

        transform.localScale += new Vector3(scaleRate, scaleRate, scaleRate);

        transform.position = player.transform.position + offset;
	}
}
