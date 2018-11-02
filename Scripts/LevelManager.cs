using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public Canvas endLevelCanvas; //Holds a canvas that will display when the level ends.
    public Canvas turnSystemCanvas; //Will be used to disable turnsystem canvas
    

    TurnSystem turnSystem; // Used to shut off turn system.
    HealTurnManager healTurnManager; //Will be used to get the turn counter for non-enemy stages
    PlayerMovement playerMovement; //Will be used to disable player movement when the level ends
    PlayerAttack playerAttack; //Will be used to disable player attacks when the level ends
    //TURN OFF ENEMY TOO!
    

   [SerializeField] bool isHealScene = false; //Flag to check if this level is a heal stage

    private void Awake()
    {
        //Fill references
        turnSystem = FindObjectOfType<TurnSystem>();
        healTurnManager = FindObjectOfType<HealTurnManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        turnSystem = FindObjectOfType<TurnSystem>();
        playerAttack = FindObjectOfType<PlayerAttack>();
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "HealScene")
        {
            isHealScene = true;
        }
        
    }

     void Update()
    {
        if(isHealScene)
        {
            HealLevelManage();
        }
    }

    void HealLevelManage()
    {
        if(healTurnManager.turnCounter >=3)
        {
            endLevelCanvas.enabled = true;
            healTurnManager.TurnOffSystem();
            playerAttack.enabled = false;
            playerMovement.enabled = false;
        }
    }

    public void EndLevelManage()
    {
        endLevelCanvas.enabled = true;
        turnSystem.TurnOffSystem();
        playerAttack.enabled = false;
        playerMovement.enabled = false;
    }



}
