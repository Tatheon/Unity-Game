using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMannager : MonoBehaviour
{
    public int redResources, greenResources;
    public int mapX, mapY;
    

    public Transform cameraPosition;

    public GameObject rangedFab;
    public GameObject meleeFab;
    public GameObject wizardFab;
    public GameObject factoryFab;
    public GameObject resourceFab;

    private string winningTeam;
    private bool isGameOver = false;
    public GameObject displayText;


    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            displayText.GetComponent<Text>().text = winningTeam+" has won the game";
            //show a reset button or something
        }
    }

    public void CreateObject(int numOfObjects)
    {
        for (int i = 0; i < numOfObjects; i ++)
        {
            int unitType = (int)Random.Range(0f, 4f);
            Vector3 postion = new Vector3(Random.Range(0, mapX), Random.Range(0, mapY));

            switch (unitType)
            {
                case 0:
                    GameObject meleeCopy = Instantiate(meleeFab, postion, Quaternion.identity);

                    Unit meleeMetta = meleeCopy.GetComponent<Unit>();
                    meleeMetta.Team = Random.Range(0, 1) == 1 ? "Green" : "Red";
                    meleeMetta.mapX = mapX;
                    meleeMetta.mapX = mapY;

                    SpriteRenderer look = meleeCopy.GetComponent<SpriteRenderer>();
                    look.color = meleeMetta.Team == "Green" ? Color.green : Color.red;
                    break;

                case 1:
                    GameObject rangeCopy = Instantiate(meleeFab, postion, Quaternion.identity);

                    Unit rangedMetta = rangeCopy.GetComponent<Unit>();
                    rangedMetta.Team = Random.Range(0, 1) == 1 ? "Green" : "Red";
                    rangedMetta.mapX = mapX;
                    rangedMetta.mapX = mapY;

                    SpriteRenderer rangedlook = rangeCopy.GetComponent<SpriteRenderer>();
                    rangedlook.color = rangedMetta.Team == "Green" ? Color.green : Color.red;
                    break;

                case 2:
                    GameObject wizardCopy = Instantiate(meleeFab, postion, Quaternion.identity);

                    Unit wizardMetta = wizardCopy.GetComponent<Unit>();
                    wizardMetta.Team = "Yellow";
                    wizardMetta.mapX = mapX;
                    wizardMetta.mapX = mapY;

                    SpriteRenderer wizardlook = wizardCopy.GetComponent<SpriteRenderer>();
                    wizardlook.color = Color.yellow;
                    break;

                case 3:
                    GameObject factoryCopy = Instantiate(factoryFab, postion, Quaternion.identity);

                    Building factoryMetta = factoryCopy.GetComponent<Building>();
                    factoryMetta.Team = Random.Range(0, 1) == 1 ? "Green" : "Red";

                    SpriteRenderer factLook = factoryCopy.GetComponent<SpriteRenderer>();
                    factLook.color = factoryMetta.Team == "Green" ? Color.green : Color.red;
                    break;

                case 4:
                    GameObject resourceCopy = Instantiate(resourceFab, postion, Quaternion.identity);

                    Building resourceMetta = resourceCopy.GetComponent<Building>();
                    resourceMetta.Team = Random.Range(0, 1) == 1 ? "Green" : "Red";

                    SpriteRenderer reLook = resourceCopy.GetComponent<SpriteRenderer>();
                    reLook.color = resourceMetta.Team == "Green" ? Color.green : Color.red;
                    break;
            }
        }  
    }

    public void CorrectCamera()
    {
        cameraPosition.position = new Vector3(mapX/2, mapY/2, -10);//sets camera to the middle of the map
    }

    public void ResetGame(int numOfObjects)
    {
        // ask for user to type mapX and mapY and number of pbjects to create
        isGameOver = false;
        Debug.Log("about to create units");
        CreateObject(numOfObjects);
        Debug.Log("made some stuff");
        CorrectCamera();
    }

    public void GameOver(string winningTeam)
    {
        if (!isGameOver)
        {
            isGameOver = true;

           this.winningTeam = winningTeam;
        }
        
    }

    public int RedResources
    {
        get { return redResources; }
        set { redResources = value; }
    }

    public int GreenResources
    {
        get { return greenResources; }
        set { greenResources = value; }
    }

    public bool IsGameOver
    {
        get { return isGameOver; }
    }
}
