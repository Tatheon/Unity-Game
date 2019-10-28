using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameMannager gameMannager;
    public GameObject healthShow;

    protected int health, maxHealth;
    protected string team;
    protected Vector3 offset;

    public float interactionTime = 0.025f;
    public float nextTimeToInteract = 0;


    public int Health//basic proerties
    {
        get { return health; }
        set { health = value; }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public string Team
    {
        get { return team; }
        set { team = value; }
    }

    public void KillIfGameover()//if the game is over then it has no putpose being on the game area, and helps to clean up the game for next round
    {
        if (gameMannager.IsGameOver)
        {
            Destroy(gameObject);
        }
        
    }
}
