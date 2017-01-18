using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    //------------------------------------------------------------------------------------------------
    //Camera switching in a game scene is handled in the CameraSwitcherScript
    //------------------------------------------------------------------------------------------------

    [SerializeField]
    Camera cam;
    [SerializeField]
    float fieldOfViewOut; 
    [SerializeField]
    float fieldOfViewIn;

    int speed = 30; // speed for controlling on PC
    float slideTouchSpeed = 0.25f; // slide speed for tablet devices

    public int mapSizeLeft;
    public int mapSizeRight;

    public float perspectiveZoomSpeed = 0.05f;        // The rate of change of the field of view in perspective mode.

    void Start()
    {
        
    }

    void Update()
    {
        //-------------------CameraMovement on PC----------------------------------------------------------
        var movement = Vector3.zero;
        
        if ((Input.GetKey("a")) && (cam.transform.position.x > mapSizeLeft) && (cam.transform.position.x < mapSizeRight + 10))
        {
            movement.x--;
        }
        if ((Input.GetKey("d")) && (cam.transform.position.x > mapSizeLeft - 10) && (cam.transform.position.x < mapSizeRight))
        {
            movement.x++;
        }
        transform.Translate(movement * speed * Time.deltaTime, Space.Self);
        //--------------------------------------------------------------------------------------------------


        //------------------Zoom functionality on tablet devices--------------------------------------------
        if (Input.touchCount == 2) // If there are two touches on the device...
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // Otherwise change the field of view based on the change in distance between the touches.
            cam.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

            // Clamp the field of view to make sure it's between 0 and 180.
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView,  fieldOfViewIn, fieldOfViewOut);
        }//-------------------------------------------------------------------------------------------------


        //-----------------------Camera movement on ANDROID device------------------------------------------
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)   
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            float moveDir = touchDeltaPosition.x * slideTouchSpeed;

            if(moveDir > 0 && (cam.transform.position.x > mapSizeLeft) && (cam.transform.position.x < mapSizeRight + 30))
            {
                transform.Translate(-touchDeltaPosition.x * slideTouchSpeed, 0 ,0);
            }

            if( moveDir < 0 && (cam.transform.position.x > mapSizeLeft - 30) && (cam.transform.position.x < mapSizeRight))
            {
                transform.Translate(-touchDeltaPosition.x * slideTouchSpeed, 0, 0);
            }
        }//-------------------------------------------------------------------------------------------------        
    }
}
