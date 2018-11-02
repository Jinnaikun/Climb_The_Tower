using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    public int maxEnemyHealth = 70; //Max amount of health the enemy has.
    public Slider enemyHealthBar; //Enemy's health bar
    public int enemyScoreToAdd; //Amount of points an enemy is worth.
    public bool isBoss = false; //Checks if enemy is boss.
    public AudioSource enemyAudio; //Source of enemy getting damage sound
    public AudioClip enemyHurtSound; //Sound of enemy getting hurt.

    [SerializeField] int currentEnemyHealth; //Enemy's current health.

    LevelManager levelManager; //Used to end the level.
    PlayerPrefManager playerPrefManager; //Adds to player's score
    SceneLoader sceneLoader; //For boss ending loading

    void Awake()
    {
        //Fill reference
        levelManager = FindObjectOfType<LevelManager>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        playerPrefManager = FindObjectOfType<PlayerPrefManager>();
    }


    void Start()
    {
        currentEnemyHealth = maxEnemyHealth;
        enemyHealthBar.maxValue = maxEnemyHealth;
        enemyHealthBar.value = maxEnemyHealth;
    }

    //Whenever the player attacks the enemy, the enemy will take damage.
    public void TakeDamage(int damageTaken)
    {
        enemyAudio.PlayOneShot(enemyHurtSound);
        currentEnemyHealth -= damageTaken;
        enemyHealthBar.value = currentEnemyHealth;

        if (currentEnemyHealth <= 0)
        {
            EnemyDeath();
        }
        
    }

    void EnemyDeath()
    {
        if (!isBoss)
        {
            playerPrefManager.CurrentScore(enemyScoreToAdd);
            levelManager.EndLevelManage();
            gameObject.SetActive(false);
        }

        else if(isBoss)
        {
            sceneLoader.WinScreen();
        }
    }
}
