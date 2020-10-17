using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour{

    public Rigidbody rigidBody;
    public float forwardforce = 800;
    public float rightForce = 800;

    void Start(){

    }

    // Update is called once per frame
    void Update(){
        MoveForward();
        MoveBackwards();
        MoveLeft();
        MoveRight();
    }

    private void MoveForward() {
        if (Input.GetKey("w")) {
            rigidBody.AddForce(0, 0, forwardforce * Time.deltaTime);
        }
    }

    private void MoveBackwards() {
        if (Input.GetKey("s")) {
            rigidBody.AddForce(0, 0, -forwardforce * Time.deltaTime);
        }
    }

    private void MoveLeft() {
        if (Input.GetKey("a")) {
            rigidBody.AddForce(-rightForce * Time.deltaTime, 0, 0);
        }
    }

    private void MoveRight() {
        if (Input.GetKey("d")) {
            rigidBody.AddForce(rightForce * Time.deltaTime, 0, 0);
        }
    }
}
