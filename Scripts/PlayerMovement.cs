using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed; // This controls how fast the player moves. More positive number, more speed.
    public float cameraRayLength; //Length of the raycast

    Vector3 moveDirection; //The direction the character is facing.
    Rigidbody playerRigidbody; //Player's rigidbody reference
    int floorMask; //Used to be detected by the turning raycast
    RaycastHit hitInfo; //Stores information that the camera ray cast hits.
    PlayerClass playerClass; //Player's class


    void Awake()
    {
        //Fill references
        floorMask = LayerMask.GetMask("Floor");
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        playerClass = FindObjectOfType<PlayerClass>();
    }

    private void Start()
    {
        if(playerClass.GetAssassin())
        {
            AssassinBuff();
        }
    }

    void FixedUpdate()
    {
        float horizontalNum = Input.GetAxisRaw("Horizontal"); //Player moving on the horizontal axis

        float verticalNum = Input.GetAxisRaw("Vertical"); //Player's movement along the vertical axis

        PlayerMove(horizontalNum,verticalNum);

        PlayerTurning();
    }

    //Controls the player's movement using the axis passed in from the player's inputs
    void PlayerMove(float h, float v)
    {
        moveDirection.Set(h, 0f, v);

        moveDirection = moveDirection.normalized * movementSpeed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + moveDirection);

    }

    //Uses raycasting to control player's turning
    void PlayerTurning()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out hitInfo, cameraRayLength, floorMask))
        {
            Vector3 playerToMouse = hitInfo.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            playerRigidbody.MoveRotation(newRotation);
        }
    }

    public void AssassinBuff()
    {
        movementSpeed *= playerClass.assassinMultiplier;
    }

}