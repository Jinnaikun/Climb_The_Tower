using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathManager : MonoBehaviour {

    [SerializeField] public bool ifHeal = false; //Checks if player healed
    [SerializeField] public bool onOverworld = false; //Checks if player is on the map/overworld
    [SerializeField] public bool isFight = true; //Checks if player faught once.

    GameObject healButton; //Tracks heal button
    GameObject bossButton; //Track boss button

    public static PathManager instance; //This path manager

    void Awake()
    {
        ///Singleton
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
  
    }

    void Start()
    {
        isFight = false;    
    }

    void Update()
    {
        //Checks if the player fought at least one enemy
        if (isFight != true)
        {
            LockBoss();
        }

        //Checks when to disable the healing room.
        if (onOverworld && ifHeal)
        {
            PlayerVisitHeal();
            onOverworld = false;
        }
       else
        {
            onOverworld = false;
        }

       
    }


    //Tracks if player healed, if they did then they cannot heal again, also very not optimal
    public void PlayerVisitHeal()
    {
        healButton = GameObject.FindGameObjectWithTag("HealPath");
        Button button = healButton.GetComponent<Button>();
        button.interactable = false;
    }

    //Tracks if player fought a boss, if they didn't lock the boss path.
    public void LockBoss()
    {
        Debug.Log("Load");
        bossButton = GameObject.FindGameObjectWithTag("BossPath");
        Button button = bossButton.GetComponent<Button>();
        button.interactable = false;
    }
}
