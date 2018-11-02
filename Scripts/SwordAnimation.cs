using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimation : MonoBehaviour {

    public Animator animator; //Animator to control animation
    float delay; //Gets delay from player Attack
    WaitForSeconds animationDelay; //Delays the toggle
    PlayerAttack playerAttack; //Takes delay time from player attack script

    void Awake()
    {
        playerAttack = FindObjectOfType<PlayerAttack>();        
    }

     void Start()
    {
        animationDelay = new WaitForSeconds(playerAttack.attackInterval);    
    }


    public void AttackAnimation()
    {

        StartCoroutine(AnimationDelay());
    }

    IEnumerator AnimationDelay()
    {
        animator.SetBool("isAttacking", true);
        yield return animationDelay;
        animator.SetBool("isAttacking", false);
    }
}
