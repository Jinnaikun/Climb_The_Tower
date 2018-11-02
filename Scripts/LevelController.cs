using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public int[] indexArray; //An array of level indices. Changable incase the level indices change.
    PathManager pathManager; //Track when player loads into Overworld.

     void Awake()
    {
        pathManager = FindObjectOfType<PathManager>();    
    }

    // Whenever the player decides to fight, they will load into a random enemy level.
    public void LoadFight()
    {
        pathManager.isFight = true; //Player has fought an enemy at least once

        int randomNum;

        randomNum = Random.Range(0, indexArray.Length);
        
        SceneManager.LoadScene(indexArray[randomNum]);
    }

    public void LoadHeal()
    {
        pathManager.ifHeal = true;
        SceneManager.LoadScene("HealScene");
    }

    public void LoadBoss()
    {
        SceneManager.LoadScene("BossScene");
    }


}
