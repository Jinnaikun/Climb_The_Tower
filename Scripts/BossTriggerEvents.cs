using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerEvents : MonoBehaviour {

    EnemyHealth enemyHealth; //Reference to trigger isBoss in the enemy health script

    private void Awake()
    {
        enemyHealth = FindObjectOfType<EnemyHealth>();
    }

    private void Start()
    {
        enemyHealth.isBoss = true;
    }
}
