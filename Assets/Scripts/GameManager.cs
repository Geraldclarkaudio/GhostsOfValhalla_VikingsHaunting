using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    MusicManager musicManager;

    private void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        gameOver = false;
    }

    public bool winGame { get; set; }
    public bool gameOver { get; set; }


    public void WinGame()
    {
        winGame = true;
        musicManager.PlayWinMX();
    }

    public void YouLose()
    {
        gameOver = true;
        musicManager.PlayGameOverMX();
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

        if(gameOver == true)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Game_Level1");
            }
        }
    }
}
