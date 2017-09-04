using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parent_position : MonoBehaviour {

    public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        this.transform.parent = player.transform;
        this.transform.localPosition = new Vector3(0, 0, 0);
    }
}
