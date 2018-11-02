using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int maxPlayerHealth; //Max player HP
    public float healEffectDuration = 9f; //How long the effect will last
    public Slider healthSlider; //Player's health slider
    public Canvas gameOverCanvas; //Used to display the canvas when the player is dead
    public AudioClip[] healthSounds; //Health sound clips
    public AudioSource healthAudioSource; //The sound comes from this.
    public GameObject healthParticleObject; //Object will have the particle system component
    public Image damageImage; //The visual feedback when the player gets hit
    public float damageFlashSpeed; //Speed in which the damage flash shows
    public Color damageFlashColor; //Color of the damage flash.
    bool isDamaged = false; //If the player is currently taking damage.

    [SerializeField] int currentPlayerHealth; //Player's current health

    PlayerPrefManager playerPrefManager; //used to record score in case of death
    GameOverTextScore gameOverTextScore; //Update game over text
    WaitForSeconds healEffectDurationWait; //Duration of the heal effects
    ParticleSystem.EmissionModule healthParticleSystem; //Particle system component to turn off and on

    void Awake()
    {
        //Fill references
        playerPrefManager = FindObjectOfType<PlayerPrefManager>();
        healthParticleSystem = healthParticleObject.GetComponent<ParticleSystem>().emission;
        
    }

    void Start()
    {
        healthParticleSystem.enabled = false;
        //Refresh display values on start
        currentPlayerHealth = maxPlayerHealth;
        healthSlider.value = currentPlayerHealth;
        healEffectDurationWait = new WaitForSeconds(healEffectDuration);
    }


    void Update()
    {

        if (isDamaged) //When the player is damaged, display the flash by setting the image color to be the same as the flash.
        {
            damageImage.color = damageFlashColor;
        }
        else //After the isDamaged is set to false, have the damage image color fade back to clear.
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, damageFlashSpeed * Time.deltaTime);
        }

        if (currentPlayerHealth > 0)
            isDamaged = false; //The damage state is always being turned off.

    }

    // This makes the player's health decrease when they take damage from hazards or enemy attacks
    public void PlayerTakeDamage(int damageTaken)
    {
        isDamaged = true;
        healthAudioSource.PlayOneShot(healthSounds[0]);
        currentPlayerHealth -= damageTaken;
        healthSlider.value = currentPlayerHealth;

        if(currentPlayerHealth <= 0)
        {
            PlayerDead();
        }
    }

    // This heals the player when they pick up a health pickup or use a healing spell.
    public void PlayerHeal(int healthToHeal)
    {
        StartCoroutine(HealParticleEffects());

        currentPlayerHealth += healthToHeal;

        healthAudioSource.PlayOneShot(healthSounds[1]);

        if(currentPlayerHealth > maxPlayerHealth)
        {
            currentPlayerHealth = maxPlayerHealth;
        }

        healthSlider.value = currentPlayerHealth;
        
    }

    // This will make the player uncontrolable when they die
    void PlayerDead()
    {
        // Make player not move and show game over screen.
        //End game check score
        playerPrefManager.Highscore();
        //Put up game over canvas to send back to main menu
        gameOverCanvas.enabled = true;
        gameOverTextScore.UpdateGameOverText();
        //Deactivate Player
        gameObject.SetActive(false);
    }
    
    //Displays the particle effects and turns them off after a set of time.
    IEnumerator HealParticleEffects()
    {
        
        healthParticleSystem.enabled = true;
        yield return healEffectDurationWait;
        healthParticleSystem.enabled = false;

    }
}
