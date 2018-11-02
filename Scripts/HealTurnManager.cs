using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealTurnManager : MonoBehaviour {

        public float timePerTurn = 10f; //Used to determine the amount of time per turn.
        public Text turnCounterText; //Text to visually keep track of the amount of turns passed.
        public GameObject turnTimerSliderObject; //The visual of the timer object
        public Slider turnTimerSlider; //Using the slider component to control the values
        public Canvas turnStartCanvas; //The button to start a turn.


        //Scripts here are used to prevent enemy and player movement
        PlayerMovement playerMovement; //Reference to player's movement script
        PlayerAttack playerAttack; //Reference to player's attack script

        [SerializeField] bool isTurnActive = false; //Flag to check if a turn is currently in play
        [SerializeField] public int turnCounter = 0; //Keeps track of the number of turns
        WaitForSeconds turnDuration; //Will count down the turn duration.


        void Awake()
        {
            //Fill references
            playerAttack = FindObjectOfType<PlayerAttack>();
            playerMovement = FindObjectOfType<PlayerMovement>();
        }

        void Start()
        {
            turnDuration = new WaitForSeconds(timePerTurn);
            turnCounterText.text = "Turn " + turnCounter;
            //When the level starts, wait until the player presses the start button.
            WaitForStart();

        }

        void Update()
        {
            //Whenever a turn is active, update the visual timer.
            if (isTurnActive)
            {
                VisualTimeManager();
            }
        }

        //Controls who moves and for how long
        IEnumerator TurnManager()
        {
            //Allow movement
            Time.timeScale = 1;

            playerMovement.enabled = true;
            playerAttack.enabled = true;
  

            turnTimerSliderObject.SetActive(true);

            yield return turnDuration;

            //Disable movement

            playerMovement.enabled = false;
            playerAttack.enabled = false;

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
