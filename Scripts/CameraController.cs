using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player; //Holds the reference of the player to follow
    public GameObject enemy; //Reference of enemy to follow
    Vector3 offset; //Holds camera offset to follow the player object

    [SerializeField] public bool isPlayer = true; //Checks if player's turn
    [SerializeField] public bool isEnemy = false; //Checks if enemy's turn

     void Start()
    {
        offset = transform.position - player.transform.position; //Takes the difference between the player and camera.
    }

     void LateUpdate()
    {
        CameraUpdate();
    }

    //Camera moves to new position over the object.
    void CameraUpdate()
    {
        if (isPlayer)
        {
            transform.position = player.transform.position + offset;
        }
        else if(isEnemy)
        {
            transform.position = enemy.transform.position + offset;
        }
    }
}
