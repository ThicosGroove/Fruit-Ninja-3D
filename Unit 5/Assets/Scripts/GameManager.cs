using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public CanvasManager canvasManager;
    public Blade blade;
    public GameObject bomb1, bomb2, bomb3;

    public bool isGameOver = false;

    public bool isEasyMode = false;
    public bool isHardMode = false;

    [SerializeField] int maxBombNumber = 3, totalScore;
    [SerializeField] int previousScore, instantScore; 
    public int comboVerify = 0, missCount, bombNumber, maxMiss = 5;

    void Update()
    {
        KeepingHighScore();

        GameOver();

        if (!blade.isSameBlade)
        {
            comboVerify = 0;
            instantScore = 0;
        }
    } 

    public void UpdateScore(int scoreToAdd)
    {
        if (blade.isSameBlade)
        {
            instantScore += scoreToAdd;

            comboVerify++;

            previousScore = instantScore * comboVerify;

            Debug.Log(previousScore);

            totalScore += previousScore;

            if (totalScore <= 0)
            {
                totalScore = 0;
            }

            canvasManager.UpdateScoreText(totalScore);

        }
    }

    public void UpdateBomb()
    {
        bombNumber ++;       

        if (bombNumber > 3)
        {
            bombNumber = 3;
        }

        if (bombNumber == 1)
        {
            bomb1.SetActive(true);
        }
        if (bombNumber == 2)
        {
            bomb2.SetActive(true);
        }
        if (bombNumber == 3)
        {
            bomb3.SetActive(true);
        }

        canvasManager.UpdateBombText();
    }

    public void HardModeCount()
    {
        if (isHardMode)
        {
            missCount++;
        }
        else if (isEasyMode)
        {
            missCount = 0;
        }      
    }

    void GameOver()
    {
        if (bombNumber == maxBombNumber || missCount == maxMiss)
        {
            isGameOver = true;
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }

    public void EasyModeButton()
    {
        isEasyMode = true;
        spawnManager.SpawnObjects();
        canvasManager.ShowHighScore();
    }

    public void HardModeButton()
    {
        isHardMode = true;
        spawnManager.SpawnObjects();
        canvasManager.ShowHighScore();
    }

    void KeepingHighScore()
    {
        if (isEasyMode)
        {
            if (PlayerPrefs.GetInt("HighScoreEasyMode") < totalScore)
            {
                PlayerPrefs.SetInt("HighScoreEasyMode", totalScore);
            }
        }
        else if (isHardMode)
        {
            if (PlayerPrefs.GetInt("HighScoreHardMode") < totalScore)
            {
                PlayerPrefs.SetInt("HighScoreHardMode", totalScore);
            }
        }
    }
}
