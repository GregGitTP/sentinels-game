using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float zoomOffset = -7; //The base z position of the camera, relative to the camera's pivot;
    public float zoomMulti = 1; //The multiplier of the camera's aspect ratio

    public float touchSense = 0.1f; //Sensitivity of touch controls
    Camera cam;
    GameObject pivot;
    // Start is called before the first frame update
    void Start() //Assigns the basic variables
    {
        cam = Camera.main;
        pivot = cam.transform.parent.gameObject;
    }
    private void Update()
    {
        transform.localPosition = new Vector3(0, 0, cam.aspect * zoomMulti + zoomOffset); // At every frame, the zoom of the camera is calculated based off the current aspect ratio multiplied by ZoomMulti, and offsetted by zoomOffset

        if (Input.touchCount > 0) //If a touch is detected
        {
            pivot.transform.localEulerAngles += new Vector3(0, Input.GetTouch(0).deltaPosition.x,0) * touchSense; //Change the Camera's pivot's Y rotation based off the touch's horizontal movement multiplied by TouchSense
        }
        if (Input.GetMouseButton(1)) //If a right click is detected
        {
            pivot.transform.localEulerAngles += new Vector3(0, Input.GetAxis("Mouse X"), 0); //Change the Camera's pivot's Y rotation based off the mouse's horizontal movement
        }
    }
}
