using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public float rangeOfAwareness; //Range of where enemy will start attacking
    public GameObject target; //The player, which will be the enemy's target
    public float attackDelay; //Delay of the enemy's attack
    public GameObject weapon; //Enemy's weapon

    [SerializeField] bool isPlayerInRange = false; //Flag showing if player is in range


    EnemyAttack enemyAttack; //Reference to the EnemyAttack script to start the attack.
    RaycastHit hitInfo; //Information from enemy's raycast.
    NavMeshAgent agent; //Allows enemy to move around map

    float timer; //Time before calling the attack function again when within range.
 

     void Awake()
    {
        //Fill references
        agent = GetComponent<NavMeshAgent>();
        enemyAttack = weapon.GetComponent<EnemyAttack>(); //Find attack script on THIS enemy

        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");

        }
    }

     void Update()
    {
        DetectPlayerRange();
        timer += Time.deltaTime; //Timer for the attack frequency keeps going

        if(!isPlayerInRange)
        {
            HuntPlayer();
        }
        else if(timer >= attackDelay && isPlayerInRange)
        {
            Attack();
        }

    }


    //Checks to see if player is within range
    void DetectPlayerRange()
    {
        float currentPlayerDistance = Vector3.Distance(transform.position, target.transform.position);

        if (currentPlayerDistance <= rangeOfAwareness)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;

        }
    }

    //AI chases player and goes to their location when they are out of attack range
    void HuntPlayer()
    {
        agent.SetDestination(target.transform.position);

        if(agent.isStopped)
        {
           agent.isStopped = false;
        }
    }

    //When in range, the AI will call an attack from the enemy attack script at certain time intervals and when in range.
    void Attack()
    {
        agent.isStopped = true;
        transform.LookAt(target.transform, Vector3.up);

        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hitInfo, rangeOfAwareness))
        {
            if(hitInfo.transform.tag == "Wall")
            {
                HuntPlayer();
            }
            else if (hitInfo.transform.tag == "Player")
            {
                timer = 0f; //Reset timer on attack
                enemyAttack.EnemyAttackPlayer();
            }
        }

    }


}
