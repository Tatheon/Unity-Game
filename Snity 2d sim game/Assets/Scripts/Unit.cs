using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract public class Unit : MonoBehaviour
{
    public GameMannager gameMannager;

    protected float closestDistance;                      // targeting and moving info
    
    public Transform closestEnemy = null;
    public float interactionTime = 0.5f;
    public float nextTimeToInteract = 0; 

    public float mapX, mapY;                  //  surrounding details
    protected Collider[] nearbyenemies;

    public GameObject healthShow;
    public Vector3 offset;
    
    protected int maxHealth, health, attack;// unit stats
    protected float speed = 4f, attackRange = 5f, searchRadius;
    protected string team;
    

    private float stopDistance = 5f;


    public void Move()
    {
        if (Vector3.Distance(transform.position, closestEnemy.position) > stopDistance)
        {
           // Debug.Log("im moving to enemy");
           Vector3 moveTo;
        
           moveTo = Vector3.Normalize(closestEnemy.transform.position - transform.position)*speed*Time.deltaTime;
           transform.position += moveTo;
        }
        else
        {
           // Debug.Log("im too close");
        }
    }

    public void RandomMove()  //moves the unit away from neerest enemy
    {
        Vector3 moveTo = (Vector3.Normalize(closestEnemy.transform.position - transform.position)*-1) * speed * Time.deltaTime;//should set the vector to away from the closest enemy
        transform.position += moveTo;
    }

    public void KillIfOutOfBounds()
    {
        if (transform.position.y < 0 | transform.position.y > mapY | transform.position.x < 0| transform.position.x > mapX)
        {
            Destroy(gameObject);
        }
    }

    public bool WithinRange(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void KillIfGameover()
    {
        if (gameMannager.IsGameOver)
        {
            Destroy(gameObject);
        }
        
    }


    public void Kill()
    {
        Destroy(gameObject);
    }

    // make method to show health %

    public abstract void Attack();//method that attacks units
    
    public abstract void DoLogic();

    public abstract void FindNearestEnemy();

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public string Team
    {
        get { return team; }
        set { team = value; }
    }

    
}
