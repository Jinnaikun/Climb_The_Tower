using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class TurnSystem : MonoBehaviour {

    public float timePerTurn = 10f; //Used to determine the amount of time per turn.
    public Text turnCounterText; //Text to visually keep track of the amount of turns passed.
    public GameObject turnTimerSliderObject; //The visual of the timer object
    public Slider turnTimerSlider; //Using the slider component to control the values
    public Canvas turnStartCanvas; //The button to start a turn.
    public Text whoTurnIsIt; // Text to state who's turn it is.


    //Scripts here are used to prevent enemy and player movement
    PlayerMovement playerMovement; //Reference to player's movement script
    PlayerAttack playerAttack; //Reference to player's attack script
    EnemyMovement enemyMovement; //Reference to enemy movement script
    EnemyAttack enemyAttack; //Reference to enemy attack script
    CameraController cameraController; //Reference to camera script to change camera views based on who's turn it is

    [SerializeField] bool isTurnActive = false; //Flag to check if a turn is currently in play
    [SerializeField] public int turnCounter = 0; //Keeps track of the number of turns
    WaitForSeconds turnDuration; //Will count down the turn duration.
    GameObject enemyObject; //The enemy
    NavMeshAgent enemyAgent; //Enemy's navmeshagent
    

    void Awake()
    {
        //Fill references
        enemyObject = GameObject.FindGameObjectWithTag("Enemy");
        playerAttack = FindObjectOfType<PlayerAttack>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        enemyAttack = FindObjectOfType<EnemyAttack>();
        enemyMovement = FindObjectOfType<EnemyMovement>();
        cameraController = FindObjectOfType<CameraController>();
        enemyAgent = enemyObject.GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        turnDuration = new WaitForSeconds(timePerTurn);
        turnCounterText.text = "Turn " + turnCounter;
        whoTurnIsIt.text = "Your Turn Player";
        WaitForStart();

    }

    void Update()
    {
        //Change the text based on who's turn it is, the player will always have an even turn starting from 0
        if(turnCounter%2 ==0)
        {
            whoTurnIsIt.text = "Your Turn Player";
        }
        else
        {
            whoTurnIsIt.text = "Enemy's Turn";
        }

        //Whenever a turn is active, update the visual timer.
        if(isTurnActive)
        {
            VisualTimeManager();
        }
    }

    //Controls who moves and for how long
    IEnumerator TurnManager()
    {
        //Allow movement
        Time.timeScale = 1;

        //Checks who's turn it is, player will always have an even numbered turn.
        if (turnCounter%2 == 0)
        {
            playerMovement.enabled = true;
            playerAttack.enabled = true;
            enemyMovement.enabled = false;
            enemyAttack.enabled = false;
            enemyAgent.isStopped = true;
            

        }
        else
        {
            enemyMovement.enabled = true;
            enemyAttack.enabled = true;
            playerMovement.enabled = false;
            playerAttack.enabled = false;
        }

        turnTimerSliderObject.SetActive(true);

        yield return turnDuration;

        //Disable movement
        if (turnCounter % 2 == 0)
        {
            playerMovement.enabled = false;
            playerAttack.enabled = false;
            cameraController.isPlayer = false;
            cameraController.isEnemy = true;
        }
        else
        {
            enemyMovement.enabled = false;
            enemyAttack.enabled = false;
            cameraController.isPlayer = true;
            cameraController.isEnemy = false;
        }

        isTurnActive = false;
        turnTimerSliderObject.SetActive(false);
        Time.timeScale = 0;
        turnStartCanvas.enabled = true;
        turnCounter++;
        turnCounterText.text = "Turn " + turnCounter;
    }

    //Pauses the time
    void WaitForStart()
    {
        Time.timeScale = 0;
    }

    //Start the current person's turn
    public void StartTurn()
    {
        turnTimerSlider.maxValue = timePerTurn;
        turnTimerSlider.value = timePerTurn;
        turnStartCanvas.enabled = false;
        isTurnActive = true;
        StartCoroutine(TurnManager());
        
    }

    //This updates the timer slider
    void VisualTimeManager()
    {
        turnTimerSlider.value -= Time.deltaTime;
    }

    public void TurnOffSystem()
    {
        turnStartCanvas.enabled = false;
    }
}
