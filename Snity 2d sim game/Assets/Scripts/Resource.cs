using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resource : Building   
{
    
    private int resourcesPerRound, resourcePoolRemaining;
    private TextMeshPro myText;
    GameObject healthPop;

    // Start is called before the first frame update
    void Start()
    {
        resourcesPerRound = 1;
        resourcePoolRemaining = 10;
        health = 40;
        maxHealth = 40;

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
        if (Time.time >= nextTimeToInteract)
        {
            nextTimeToInteract = Time.time + 1f / interactionTime;
            if (team == "Green")
            {
                gameMannager.GreenResources = MakeResource();
            }
            else
            {
                gameMannager.RedResources = MakeResource();
            }
            
        }
    }

    public int MakeResource()
    {
        if (resourcePoolRemaining > 0)
        {
            
            resourcePoolRemaining -= resourcesPerRound;//generate recource then subtract what was generated from how much it can produce in total
            return resourcesPerRound;//add what was generated
        }
        else
        {
            return 0;
        }
    } 
}
