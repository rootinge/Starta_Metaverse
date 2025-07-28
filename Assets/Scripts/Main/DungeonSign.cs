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


    [Header("던전 정보, 점수")]
    [SerializeField]
    private DungeonType dungeonType;

    [Header("UI")]
    [SerializeField] private GameObject dungeonSignUI;
    [SerializeField] private Text dugeonNameText;
    [SerializeField] private Text bastScoreText;
    [SerializeField] private Text recentlyScoreText;
    [SerializeField] private Text recordText;

    private void Awake()
    {
        dungeonSignUI.SetActive(false);
        recordText.gameObject.SetActive(false);
    }



    void Start()
    {
        dugeonNameText.text = dungeonType.ToString();

        // Enum 타입에 따라 최근스코어, 베스트 스코어 설정 (던전이 늘어나면 추가해야 함)
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
        NewRecord(float.Parse(recentlyScoreText.text), float.Parse(bastScoreText.text));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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

    private void NewRecord(float bast, float recently)
    {
        if (bast != 0 && bast == recently)
        {
            recordText.gameObject.SetActive(true);
        }
    }


}
