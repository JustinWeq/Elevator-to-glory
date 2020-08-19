using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_script : MonoBehaviour
{
    public float CAMERA_SPEED = 5;
    public float ZOOM_SPEED = 150;
    public float SCROLL_EDGE = 0.1f;
    public float CAMERA_START_HEIGHT = 15;
    public float MAX_HEIGHT = 100;
    public float MIN_HEIGHT = 5;
    private unit_control_script active_unit;
    // Start is called before the first frame update
    void Start()
    {
        //move to the starting height and player location
        GameObject grug = GameObject.Find("Grug");
        transform.position = new Vector3(0, CAMERA_START_HEIGHT, 0);
    }

    public void SetActiveUnit(unit_control_script unit)
    {
        active_unit = unit;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Vector3.zero;
        //get a multiplyer for the camera move speed
        float speedMult = (transform.position.y / CAMERA_START_HEIGHT) * CAMERA_SPEED;
        //move the camera with the arrow keys
        if (Input.GetKey("left"))
        {
            newPos += Vector3.left * speedMult;
        }
        if (Input.GetKey("right"))
        {
            newPos += Vector3.right * speedMult;
        }
        if (Input.GetKey("up"))
        {
            newPos += Vector3.forward * speedMult;
        }
        if (Input.GetKey("down"))
        {
            newPos += Vector3.back * speedMult;
        }


        newPos += Vector3.down * Input.mouseScrollDelta.y * ZOOM_SPEED * (transform.position.y / CAMERA_START_HEIGHT);


        //check to see if mouse is on the edge of the screen and it is move it
        if (Input.mousePosition.x < Screen.width * SCROLL_EDGE)
            newPos += Vector3.left * speedMult;
        if (Input.mousePosition.x > Screen.width - Screen.width * SCROLL_EDGE)
            newPos += Vector3.left * -speedMult;
        if (Input.mousePosition.y < Screen.height * SCROLL_EDGE)
            newPos += Vector3.forward * -speedMult;
        if (Input.mousePosition.y > Screen.height - Screen.height * SCROLL_EDGE)
            newPos += Vector3.forward * speedMult;


        //re center the camera on the player unit if the player presses space
        if (Input.GetKeyDown("space"))
        {
            if(active_unit != null)
            {
                newPos = new Vector3(active_unit.transform.position.x, newPos.y, active_unit.transform.position.z);
                newPos.z *= Time.deltaTime;
                transform.SetPositionAndRotation(newPos+new Vector3(0,transform.position.y,0), transform.rotation);
            }
            else
            {
                transform.SetPositionAndRotation(transform.position + (newPos * Time.deltaTime), transform.rotation);
            }
        }
        else
        {
            transform.SetPositionAndRotation(transform.position + (newPos * Time.deltaTime), transform.rotation);
        }

        //print the mouse position and the screen width
        //Debug.Log("Mouse( " + Input.mousePosition.x + "," + Input.mousePosition.y + ")");
        //transform.SetPositionAndRotation(transform.position + (newPos * Time.deltaTime), transform.rotation);
        if (transform.position.y < MIN_HEIGHT)
        {
            transform.position = new Vector3(transform.position.x, MIN_HEIGHT, transform.position.z);
        }
        if (transform.position.y > MAX_HEIGHT)
        {
            transform.position = new Vector3(transform.position.x, MAX_HEIGHT, transform.position.z);
        }
    }
}
