using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DungeonSign : MonoBehaviour
{
    public enum DungeonType
    {
        FireWater,
        FlappyPlane,
    }
    [SerializeField]
    private string dungeonSceneName;


    [Header("���� ����, ����")]
    [SerializeField]
    private DungeonType dungeonType;

    [Header("UI")]
    [SerializeField] private GameObject dungeonSignUI;
    [SerializeField] private Text dugeonNameText;
    [SerializeField] private Text bastScoreText;
    [SerializeField] private Text recentlyScoreText;
    




    void Start()
    {
        dugeonNameText.text = dungeonType.ToString();

        // Enum Ÿ�Կ� ���� �ֱٽ��ھ�, ����Ʈ ���ھ� ���� (������ �þ�� �߰��ؾ� ��)
        switch (dungeonType)
        {
            case DungeonType.FireWater:
                recentlyScoreText.text = DataManager.Instance.fireWaterRecentlyScore.ToString("F2");
                bastScoreText.text = DataManager.Instance.fireWaterBastScore.ToString("F2");
                break;
            case DungeonType.FlappyPlane:
                recentlyScoreText.text = DataManager.Instance.flappyPlaneRecentlyScore.ToString();
                bastScoreText.text = DataManager.Instance.flappyPlaneBestScore.ToString();
                break;
            default:
                Debug.LogError("Unknown dungeon type: " + dungeonType);
                break;
        }

        dungeonSignUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            dungeonSignUI.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            dungeonSignUI.SetActive(false);
    }

    public void OnGameStart()
    {
        MainGameManager.Instance.PlayerTransformStorage();
        SceneManager.LoadScene(dungeonSceneName);
    }
}
