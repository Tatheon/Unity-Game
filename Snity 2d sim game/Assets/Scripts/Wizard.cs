using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wizard : Unit
{
    private TextMeshPro myText;
    GameObject healthPop;

    // Start is called before the first frame update
    void Start()
    {
        
        
        attackRange = 4f;
        attack = 4;
        Health = 20;
        MaxHealth = 20;

        searchRadius = mapX * mapX + mapY * mapY;
        Mathf.Sqrt(searchRadius);//sets the search radious to be big enought depending on how big the map is.

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
        if (closestEnemy == null)
        {
            gameMannager.GameOver(team);
            Destroy(gameObject);
        }
        else
        {
            float healthPercent = maxHealth / health;
            if (healthPercent <= 0.5)
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
        nearbyenemies = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider neerbyEneny in nearbyenemies)
        {
            if (neerbyEneny.tag != "Wizard")
            {
                Unit unit = neerbyEneny.GetComponent<Unit>();

              if (unit != null )
              {
                  unit.Health -= attack;
                  Debug.Log("attacked:" + neerbyEneny);
                  if (unit.Health <= 0)
                  {
                      unit.Kill();
                  }
              }
              else
              {
                  Debug.Log("conditions not ready");
              }

            }
            
        }
    }

    public override void FindNearestEnemy()
    {
        nearbyenemies = Physics.OverlapSphere(transform.position, searchRadius);// you may need to change all the coliders on the prefab
        closestDistance = float.MaxValue;
        closestEnemy = null;
       // Debug.Log("colloders: " + collider.Length+" search range: "+ searchRadius );

        foreach (Collider nearbyEnemy in nearbyenemies)
        {
            Transform Enemytransform = nearbyEnemy.GetComponent<Transform>();
            
            //Debug.Log("" + Enemytransform);
            if (Enemytransform != transform & nearbyEnemy.tag == "Unit")
            {
                float distance = Vector3.Distance(transform.position, Enemytransform.position);

                   if ( distance < closestDistance)
                   {
                       closestDistance = distance;
                       closestEnemy = Enemytransform;
                   }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
}
