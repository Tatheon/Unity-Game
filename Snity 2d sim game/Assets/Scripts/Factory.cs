using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Factory : Building
{
    

    public GameObject rangedFab;
    public GameObject meleeFab;

    private readonly float offsetY = 3f;//info for helth being shown
    private TextMeshPro myText;
    GameObject healthPop;

    private void Start()// setup of object
    {
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
        KillIfGameover();//clean up operation
        if (Time.time >= nextTimeToInteract)//controlls whan a unit will be made
        {
            nextTimeToInteract = Time.time + 1f / interactionTime;
            if (team == "Green")
            {
                if (gameMannager.GreenResources>1)
                {
                    gameMannager.GreenResources -= 1;
                    MakeUnit();
                }    
            }
            else
            {
                if (gameMannager.GreenResources > 1)
                {
                    gameMannager.RedResources -= 1;
                    MakeUnit();
                } 
            }
        }
    }

    public void MakeUnit()//where the magic happens
    {
        int unitType = (int)Random.Range(0f,1f);
        if (unitType == 0)
        {
            //mack melee
            Vector3 postion = new Vector3(transform.position.x, transform.position.y+offsetY);
            GameObject meleeCopy = Instantiate(meleeFab, postion , Quaternion.identity);

            Unit meleeMetta = meleeCopy.GetComponent<Unit>();
            meleeMetta.Team = team;
            meleeMetta.mapX = gameMannager.mapX;
            meleeMetta.mapX = gameMannager.mapY;

            SpriteRenderer look = meleeCopy.GetComponent<SpriteRenderer>();
            look.color = team == "Green" ? Color.green : Color.red;
        }
        else
        {
            //make range
            Vector3 postion = new Vector3(transform.position.x, transform.position.y + offsetY);
            GameObject rangeCopy = Instantiate(rangedFab, postion, Quaternion.identity);

            Unit rangedMetta = rangeCopy.GetComponent<Unit>();
            rangedMetta.Team = team;
            rangedMetta.mapX = gameMannager.mapX;
            rangedMetta.mapY = gameMannager.mapY;

            SpriteRenderer look = rangeCopy.GetComponent<SpriteRenderer>();
            look.color = team == "Green" ? Color.green : Color.red;
        }
    }
}
