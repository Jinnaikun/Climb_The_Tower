using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public int enemyDamageAmount; //How much damage the enemy will do
    public float attackDelayAmount; //How much delay in between each attack
    public AudioSource enemyAudio; //Source of enemy hitting
    public AudioClip enemyHitSound; //Sound of enemy attacking

    [SerializeField] bool isAttacking = false; //Checks if the enemy is attacking

    Collider meleeCollider; //Enemy's hitbox
    PlayerHealth playerHealth; //Player health script to make the player take damage
    WaitForSeconds hitboxDelay; //Delay on enemy hitbox/melee collider

    void Awake()
    {
        //Fill references
        playerHealth = FindObjectOfType<PlayerHealth>();
        meleeCollider = GetComponent<Collider>();
    }

    void Start()
    {
        hitboxDelay = new WaitForSeconds(attackDelayAmount);     
    }

    private void OnTriggerStay(Collider other)
    {
        if(isAttacking && other.tag == "Player")
        {
            playerHealth.PlayerTakeDamage(enemyDamageAmount);
            meleeCollider.enabled = !meleeCollider.enabled;

        }
        
    }

    public void EnemyAttackPlayer()
    {
        enemyAudio.PlayOneShot(enemyHitSound);
        isAttacking = true;
        StartCoroutine(MeleeHitboxTimer());
    }

    IEnumerator MeleeHitboxTimer()
    {

        yield return hitboxDelay;
        isAttacking = false;
        meleeCollider.enabled = !meleeCollider.enabled;
    }
}
