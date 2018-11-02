using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePowerUps : MonoBehaviour {

    public float rotateSpeed; //Changeable rotate speed for the collectable

	void Update ()
    {
        transform.Rotate(new Vector3(15, 30, 45) * rotateSpeed * Time.deltaTime); //PowerUps rotate
	}
}
