using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance { get { return instance;}}

    public float fireWaterBastScore;
    public float fireWaterRecentlyScore;

    public int flappyPlaneBestScore;
    public int flappyPlaneRecentlyScore;

    [SerializeField]
    public Vector3? oldPlayerPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        fireWaterBastScore = 0;
        fireWaterRecentlyScore = 0;
        flappyPlaneBestScore = 0;
        flappyPlaneRecentlyScore = 0;
        oldPlayerPos = null;
    }

}
