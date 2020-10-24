using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    //Serializefield makes a private variable visible in the inspector (much more complicated, but that's the short version)
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] float moveSmoothTime = 0.15f;
    [SerializeField] float mouseSmoothTime = 0.03f;

    [SerializeField] bool lockCursor = true;

    private float cameraPitch = 0.0f;
    private float velocityY = 0.0f;
    private CharacterController controller = null;

    private Vector2 currentDir = Vector2.zero;
    private Vector2 currentDirVelocity = Vector2.zero;
    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 currentMouseDeltaVelocity = Vector2.zero;
    // Start is called before the first frame update
    void Start(){

        controller = GetComponent<CharacterController>();

        //If lockCursor is true it locks the cursor to the center of the screen and makes it invisible
        if (lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


    // Update is called once per frame
    void Update(){
        MouseLookUpdate();
        MovementUpdate();
    }


    private void MouseLookUpdate() {
        //Returns the axis of the mouse placement
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        //Smoothing mouselook method.
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        //Inverts the camera axis value (unity camera is defaulted to "look upwards" with a negative value)
        cameraPitch -= currentMouseDelta.y * mouseSensitivity;

        //Clamps the axis of the camera (meaning to lock it in place) if the min/max axis value is recieved.
        //Makes it so the camera cant rotate more than 90 degrees up or down.
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        //Rotates the camera up and down, based on the mouse's y position
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        //Rotates the player left and right, based on mouse x position
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);

    }

    private void MovementUpdate() {
        //Stores the input of a movement-keypress
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Ensures the value when moving diagonally also is 1, to ease implementation of a movement speed and make it so the speed dosen't increase when moving sideways.
        targetDir.Normalize();

        //Smoothing movement method.
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        //Checks the player is grounded if he is nothing is applied if he isn't he's affected by the gravity value.
        if (controller.isGrounded) {
            velocityY = 0.0f;
        }
        else {
            velocityY += gravity * Time.deltaTime;
        }

        //Stores the value of which movementkeys are pressed and applies gravity.
        //Vector3.up is applied instead of Vector3.down, since the velocityY is a negative value. Otherwise the player would shoot into the sky.
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);


    }
}
