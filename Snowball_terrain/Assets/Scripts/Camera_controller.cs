using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_controller : MonoBehaviour {

    //public GameObject player;
    public Transform player;
    public float turnSpeed = 4.0f;

    private Vector3 offset;

	// Use this for initialization
	void Start ()
    {
        offset = transform.position - player.transform.position;
        //offset = new Vector3(player.position.x, player.position.y + 1.0f, player.position.z + 1.0f);
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        //transform.position = player.transform.position + offset;
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}
