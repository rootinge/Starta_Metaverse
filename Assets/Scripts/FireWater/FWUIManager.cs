using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FWUIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Button restartButton;

    
    void Start()
    {
        if (restartButton == null)
        {
            Debug.LogError("restart text is null");
        }

        if (scoreText == null)
        {
            Debug.LogError("score text is null");
        }
        restartButton.gameObject.SetActive(false);
    }


    public void SetRestart()
    {
        restartButton.gameObject.SetActive(true);
    }

    public void UpdateScore(float score)
    {
        scoreText.text = score.ToString("F2");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
