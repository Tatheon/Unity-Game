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


    public int Health
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

    public void KillIfGameover()
    {
        if (gameMannager.IsGameOver)
        {
            Destroy(gameObject);
        }
        
    }
}
