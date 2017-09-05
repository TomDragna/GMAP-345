using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape")) Application.Quit();
    }

    public void PlayGame ()
    {
        SceneManager.LoadScene("MooreTrackUpdate");
    }
}
