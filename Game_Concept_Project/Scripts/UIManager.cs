using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;
    public Text scoreText;
    public Text title;
    public GameObject titleText;
    public int score;

    public void updateLives(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void updateScore()
    {
        score += 5;
        scoreText.text = "Score: " + score;
    }

    public void showTitle()
    {
        titleText.SetActive(true);
    }

    public void hideTitle()
    {
        titleText.SetActive(false);
        scoreText.text = "Score: ";
        score = 0;
    }

}
