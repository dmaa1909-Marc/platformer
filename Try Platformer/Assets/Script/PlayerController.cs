using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    //Serializefield makes a private variable visible in the inspector (much more complicated, but that's the short version)
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 6.0f;

    [SerializeField] bool lockCursor = true;

    private float cameraPitch = 0.0f;
    private CharacterController controller = null;

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
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        //Inverts the camera axis value (unity camera is defaulted to "look upwards" with a negative value)
        cameraPitch -= mouseDelta.y * mouseSensitivity;

        //Clamps the axis of the camera (meaning to lock it in place) if the min/max axis value is recieved.
        //Makes it so the camera cant rotate more than 90 degrees up or down.
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        //Rotates the camera up and down, based on the mouse's y position
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        //Rotates the player left and right, based on mouse x position
        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);

    }

    private void MovementUpdate() {
        //Returns the input of a movement-keypress
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Ensures the value when moving diagonally also is 1, to ease implementation of a movement speed and make it so the speed dosen't increase when moving sideways.
        inputDir.Normalize();

        //Moves the player object based on the value of which movementkeys are pressed.
        Vector3 velocity = (transform.forward * inputDir.y + transform.right * inputDir.x) * walkSpeed;

        controller.Move(velocity * Time.deltaTime);
    }
}
