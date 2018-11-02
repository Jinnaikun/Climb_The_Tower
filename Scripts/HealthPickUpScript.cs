using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUpScript : MonoBehaviour {

    public int amountToHeal; //Amount of health to heal
    PlayerHealth playerHealth; //Reference to player's health script

    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();    
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerHealth.PlayerHeal(amountToHeal);
            Destroy(gameObject);
        }
    }
}
