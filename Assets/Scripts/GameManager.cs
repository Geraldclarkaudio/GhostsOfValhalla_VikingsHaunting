using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("NO GAME MANAGER MAN!");
            }    
            return instance;
        }
    }

    public bool winGame { get; set; }
    public bool gameOver { get; set; }


    public void WinGame()
    {
        winGame = true;
    }

    public void YouLose()
    {
        gameOver = true;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
