using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FWGameManager : MonoBehaviour
{
    private static FWGameManager instance;
    public static FWGameManager Instance { get { return instance; } }
    private FWUIManager uiManager;


    public bool isDied = false;
    public bool isGameClear = false;

    [SerializeField]
    private float timeScore = 0f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        uiManager = FindObjectOfType<FWUIManager>();
    }

    private void Update()
    {
        if (isDied || isGameClear) return;

        timeScore += Time.deltaTime;
        uiManager.UpdateScore(timeScore);
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ScoreStorage()
    {
        if(DataManager.Instance.fireWaterBastScore == 0)
        {
            DataManager.Instance.fireWaterBastScore = timeScore;
        }
        else if (DataManager.Instance.fireWaterBastScore > timeScore)
        {
            DataManager.Instance.fireWaterBastScore = timeScore;
        }
        DataManager.Instance.fireWaterRecentlyScore = timeScore;
        Debug.Log("점수 저장 완료: " + timeScore);
    }

    public void GameOver()
    {
        isDied = true;
        uiManager.SetRestart();
    }
}

