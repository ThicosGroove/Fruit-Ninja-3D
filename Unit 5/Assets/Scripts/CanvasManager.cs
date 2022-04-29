using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public GameManager gameManager;
    public Blade blade;

    public GameObject comboTextUI;
    GameObject newComboTextUiPrefab; 
    public TMP_Text comboText;
    public TMP_Text scoreText;
    public TMP_Text bombsText;
    public TMP_Text missesText;
    public TMP_Text highScoreText;
    public GameObject gameOverText;
    public GameObject hardButton;
    public GameObject easyButton;
    public GameObject restartButton;

    public GameObject bomb1, bomb2, bomb3;

    public int score;
    public int bombNumber;

    void Start()
    {
        UpdateScoreText(0);

        UpdateBombText();
      
        bomb1.SetActive(false);
        bomb2.SetActive(false);
        bomb3.SetActive(false);

        gameOverText.SetActive(false);
        restartButton.SetActive(false);
    }

    void Update()
    {
        StartGameText();

        UpdateMissesText();
        //ShowHighScore();

        GameOverText();
    }

    public void UpdateScoreText(int scoreToAdd)
    {
        scoreText.text = "Score: " + scoreToAdd;
    }

    public void UpdateBombText()
    {
        bombsText.text = "Bombs: ";
    }

    void UpdateMissesText()
    {
        if (gameManager.isHardMode)
        {
            missesText.text = "Misses: " + gameManager.missCount + " / " + gameManager.maxMiss ;
        }
        else if (gameManager.isEasyMode)
        {
            missesText.text = "";
        }
    }

    public void ComboTextInstantiate(Vector3 targetPos)
    {
        if (gameManager.comboVerify > 1 && blade.isSameBlade)
        {
            newComboTextUiPrefab = Instantiate(comboTextUI, targetPos, Quaternion.identity);

            newComboTextUiPrefab.GetComponent<TMP_Text>().text = gameManager.comboVerify + "x Combo";
        }   
    }

    void StartGameText()
    {
        if (gameManager.isHardMode)
        {
            easyButton.SetActive(false);
            hardButton.SetActive(false);    
        }
        else if (gameManager.isEasyMode)
        {
            easyButton.SetActive(false);
            hardButton.SetActive(false);
        }
    }

    void GameOverText()
    {
        if (gameManager.isGameOver)
        {
            gameOverText.SetActive(true);
            restartButton.SetActive(true);
        }
    }

    public void ShowHighScore()
    {
        if (gameManager.isEasyMode)
        {
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScoreEasyMode");
        }
        else if (gameManager.isHardMode)
        {
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScoreHardMode");
        }
    }
}
