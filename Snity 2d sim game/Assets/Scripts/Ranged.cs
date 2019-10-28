using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ranged : Unit
{
    private TextMeshPro myText;
    GameObject healthPop;
    // Start is called before the first frame update
    void Start()
    {
        health = 10;
        maxHealth = 10;
        attackRange = 8f;
        searchRadius = mapX * mapX + mapY * mapY;
        Mathf.Sqrt(searchRadius);//sets the search radious to be big enought depending on how big the map is.
        attack = 5;

        offset = new Vector3(0, transform.position.y + 0.01f);

        healthPop = Instantiate(healthShow, transform.position+offset, Quaternion.identity);
        myText = healthPop.GetComponent<TextMeshPro>();

    }

    // Update is called once per frame
    void Update()
    {
        healthPop.transform.position = transform.position + offset;
        myText.text = health + " / " + maxHealth;
        KillIfGameover();
        DoLogic();
    }

    public override void DoLogic()
    {
        FindNearestEnemy();
        if (closestEnemy = null)
        {      
            gameMannager.GameOver(team);
            Destroy(gameObject);
        }
        else
        {
            float healthPercent = maxHealth / health;
            if (healthPercent <= 0.25)
            {
                RandomMove();
            }
            else
            {
                Move();
                KillIfOutOfBounds();
                if (Time.time >= nextTimeToInteract)
                {
                    nextTimeToInteract = Time.time + 1f / interactionTime;
                    Attack();
                }
            }
        }
    }

    public override void Attack()
    {
        if (closestEnemy.tag == "Building")
        {
            Building building = closestEnemy.GetComponent<Building>();//you may need to do change it to get factory(if null get resource)
            building.Health -= attack;
        }
        else
        {
            Unit unit = closestEnemy.GetComponent<Unit>();
            unit.Health -= attack;
        }
    }

    public override void FindNearestEnemy()
    {
        nearbyenemies = Physics.OverlapSphere(transform.position, searchRadius);
        closestDistance = float.MaxValue;
        closestEnemy = null;
        // Debug.Log("colloders: " + collider.Length+" search range: "+ searchRadius );

        foreach (Collider nearbyEnemy in nearbyenemies)
        {
            Transform Enemytransform = nearbyEnemy.GetComponent<Transform>();

            Building buildingEnemy = nearbyEnemy.GetComponent<Building>();
            Unit unitEnemy = nearbyEnemy.GetComponent<Unit>();
            string enemyTeam = "";

            if (nearbyEnemy.tag == "Building")
            { enemyTeam = buildingEnemy.Team; }
            else
            { enemyTeam = unitEnemy.Team; }

            //Debug.Log("" + Enemytransform);
            if (Enemytransform != transform & enemyTeam != team)
            {
                float distance = Vector3.Distance(transform.position, Enemytransform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = Enemytransform;
                }
            }
        }
    }
}
