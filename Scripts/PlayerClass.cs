using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerClass : MonoBehaviour {

    public static PlayerClass instance; //One instance of player's class
    public float assassinMultiplier = 2f; //Attack speed muliplier
    public int warriorMultiplier = 2; //Attack damage multiplier 

   [SerializeField] public bool isAssassin = false; //Holds switch to see if player picks this class
   [SerializeField] public bool isWarrior = false; //Holds switch to see if player picks this class

    void Awake()
    {
        ///Singleton
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }

        else if(instance!=this)
        {
            Destroy(gameObject);
        }
    }

    public void AssassinClass()
    {
        isAssassin = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void WarriorClass()
    {
        isWarrior = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void VanillaClass()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    ///Getters
    public bool GetWarrior()
    {
        return isWarrior;
    }

    public bool GetAssassin()
    {
        return isAssassin;
    }

}
