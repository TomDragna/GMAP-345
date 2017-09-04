using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_controller : MonoBehaviour {

    //public GameObject player;
    public GameObject player;
    public float turnSpeed = 4.0f;


    private float minDistance;
    private float distance;
    private float sRate;
    private Vector3 offset;


    private Vector3 cameraTarget;   //Where camera should look
    public float cTargetyOffset;    //how high above character to look

    private bool cameraToEnd = false;   //variables for camera reposition at end
    public Transform endCameraTarg;
    public Transform endCameraPos;
    public float endSpeed;

    void Start ()
    {
        minDistance = Vector3.Distance(this.transform.position, player.transform.position);

        /*GameObject SnowBall = GameObject.Find("SnowBall");
        Player_controller s_controller = SnowBall.GetComponent<Player_controller>();
        sRate = s_controller.scaleRate;*/


        offset = transform.position - player.transform.position;
        //offset = new Vector3(player.position.x, player.position.y + 1.0f, player.position.z + 1.0f);
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        //for testing -->  if (Input.GetKeyDown("return")) CameraSetToEnd();

        if (!cameraToEnd) {
            //Player_controller s_controller = player.GetComponent<Player_controller>();
            float scaleRate = player.transform.localScale.y;

            distance = minDistance + scaleRate;
        
            transform.position = (offset).normalized * distance + player.transform.position;

            //Debug.Log(distance);
            //transform.position = player.transform.position + offset;
            //offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
            //transform.position = player.transform.position + offset;

            cameraTarget = new Vector3(player.transform.position.x, player.transform.position.y + cTargetyOffset, player.transform.position.z);
            transform.LookAt(cameraTarget);


        //for camera reposition at end of track
        } else {
            transform.position = Vector3.MoveTowards(transform.position, endCameraPos.position, endSpeed * Time.deltaTime);
            transform.LookAt(endCameraTarg);
        }
    }

    // use function when snowball enters end trigger or however we decide end
    public void CameraSetToEnd() {
        transform.parent = null;
        cameraToEnd = true;
        transform.GetChild(1).gameObject.SetActive(false); // !!! fog needs to disappear slowly, be deparented from camera or disabled (as is the current solution) 
    }
}