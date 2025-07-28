using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPGameManager : MonoBehaviour
{
    static FPGameManager gameManager;

    public static FPGameManager Instance { get { return gameManager; } }

    private int currentScore = 0;

    //[SerializeField]
    //private 

    FPUIManager uiManager;
    public FPUIManager UIManager { get { return uiManager; } }
    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<FPUIManager>();
    }
    
    private void Start()
    {
        uiManager.UpdateScore(0);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        uiManager.SetRestart();
    }

    void ScoreStorage()
    {
        if (DataManager.Instance.flappyPlaneBestScore < currentScore)
        {
            DataManager.Instance.flappyPlaneBestScore = currentScore;
        }
        DataManager.Instance.flappyPlaneRecentlyScore = currentScore;
    }

    public void RestartGame()
    {
        ScoreStorage();
        SceneManager.LoadScene("MainScene");
    }

    public void AddScore(int score)
    {
        currentScore += score;
        Debug.Log("Score: " + currentScore);
        uiManager.UpdateScore(currentScore);
    }
}
