using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public GameMannager mannager;
    public GameObject canvasMenu;

    public GameObject InputfieldX;
    public GameObject InputfieldY;
    public GameObject InputfieldOb;

    public void Update()
    {
        if (mannager.IsGameOver)
        {
            canvasMenu.SetActive(true);
        }
    }

    public void StartGame()
    {
        mannager.mapX = (int.Parse)(InputfieldX.GetComponent<InputField>().text);
        mannager.mapY = (int.Parse)(InputfieldY.GetComponent<InputField>().text); 
        mannager.ResetGame((int.Parse)(InputfieldOb.GetComponent<InputField>().text));
        canvasMenu.SetActive(false);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
