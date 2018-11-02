using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

    public int primaryAttackDamage = 10; //The amount of damage the player does with their primary
    public int maxAmnoAmount = 3; //Max amount of amno on the player's secondary weapon
    public float maxPitch; //Highest pitch for the audio
    public float minPitch; //Lowest pitch for the audio
    public float attackInterval = 1f; //Time between the player's attacks
    public float hitboxInterval = .5f; //Time between active hitboxes
    public Text playerAmnoText; //Display secondary weapon amno
    public AudioClip [] swordSounds; //Plays sword sounds
    public AudioSource soundEffectSource; //Player sound effect source

    [SerializeField] int secondaryAttackDamage = 25; //The amount of damage the player does with their secondary
    [SerializeField] int currentAmnoAmount; //The amount of amno the player has on their secondary weapon
    [SerializeField] bool isAttacking = false; //Checks when the player is attacking
    [SerializeField] bool isPrimary; //Checks which weapon the player is using
   

    float attackDelay; //Wait for the attack interval
    WaitForSeconds hitboxDelay; //Wait for hitbox to deactivate
    EnemyHealth enemyHealth; //Reference to the enemy's health to do damage.
    SwordAnimation swordAnimation;//Plays the sword animation
    Collider meleeCollider; //Player's melee collider
    PlayerClass playerClass; // Upgrade's player's stats based on class

     void Awake()
    {
        //Fill references
        enemyHealth = FindObjectOfType<EnemyHealth>();
        meleeCollider = gameObject.GetComponent<Collider>();
        swordAnimation = FindObjectOfType<SwordAnimation>();
        playerClass = FindObjectOfType<PlayerClass>();
    }

    void Start()
    {
        
        if(playerClass.GetWarrior())
        {
            Debug.Log("GOT WARRIOR BUFF");
            WarriorBuff();
        }
        else if(playerClass.GetAssassin())
        {
            AssassinBuff();
        }
        //Set the delay timer and refresh the UI
        hitboxDelay = new WaitForSeconds(hitboxInterval);
        currentAmnoAmount = maxAmnoAmount;
        playerAmnoText.text = currentAmnoAmount + " / " + maxAmnoAmount;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (!isAttacking && Time.time > attackDelay)
            {
                attackDelay = Time.time + attackInterval;
                PrimaryAttack();
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            if(!isAttacking && Time.time > attackInterval)
            {
                attackDelay = Time.time + attackInterval;
                SecondaryAttack();
            }
            
        }
    }

    //Player's main attack on left click.
    void PrimaryAttack()
    {
        soundEffectSource.pitch = Random.Range(minPitch, maxPitch);
        isAttacking = true;
        swordAnimation.AttackAnimation(); // Call animation
        soundEffectSource.PlayOneShot(swordSounds[0]); // Play sound
        isPrimary = true;
        StartCoroutine(MeleeHitboxTimer()); //"How long the hit box last" timer
        
    }

    //Player's secondary attack/buff on right click.
    void SecondaryAttack()
    {
        if (currentAmnoAmount > 0)
        {
            soundEffectSource.pitch = Random.Range(minPitch, maxPitch);
            isAttacking = true;
            swordAnimation.AttackAnimation(); // Call animation
            soundEffectSource.PlayOneShot(swordSounds[1]); //Play sound
            isPrimary = false;
            currentAmnoAmount -= 1;
            playerAmnoText.text = currentAmnoAmount + " / " + maxAmnoAmount; //Update amno text
            StartCoroutine(MeleeHitboxTimer()); //"How long the hit box last" timer
        }
        else
        {
            soundEffectSource.PlayOneShot(swordSounds[2]);
        }

    }

    void OnTriggerStay(Collider other)
    {
      if(isAttacking && other.tag == "Enemy")
        {
            if(isPrimary)
            {
                enemyHealth.TakeDamage(primaryAttackDamage);
                meleeCollider.enabled = !meleeCollider.enabled;
            }
            else
            {
                enemyHealth.TakeDamage(secondaryAttackDamage);
                meleeCollider.enabled = !meleeCollider.enabled;
            }
          
        }
    }

    IEnumerator MeleeHitboxTimer()
    {

        yield return hitboxDelay;
        isAttacking = false;
        meleeCollider.enabled = !meleeCollider.enabled;
    }

    public void WarriorBuff()
    {
        primaryAttackDamage *= playerClass.warriorMultiplier;
        maxAmnoAmount *= playerClass.warriorMultiplier;
        currentAmnoAmount = maxAmnoAmount;
        playerAmnoText.text = currentAmnoAmount + " / " + maxAmnoAmount;

    }

    public void AssassinBuff()
    {
        hitboxDelay = new WaitForSeconds(hitboxInterval * playerClass.assassinMultiplier);
    }
}
